using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using AutoMapper;
using AWS.Serverless.Common.Mapper;
using AWS.Serverless.Common.Models;
using AWS.Serverless.DBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AWSServerless
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
			//services.AddAutoMapper(typeof(Startup));
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MapperProfile());
			});

			IMapper mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);

			var dynamoDbConfig = Configuration.GetSection("DynamoDb");
			var runLocalDynamoDb = dynamoDbConfig.GetValue<bool>("LocalMode");

			//AWS Setting
			var awsOptions = Configuration.GetAWSOptions();

			if (runLocalDynamoDb)
			{
				var awsSettings = Configuration.GetSection("AWS");
				awsOptions.Profile = "test";
				awsOptions.DefaultClientConfig.ServiceURL = "http://localhost:8080";

				awsOptions.Credentials = new BasicAWSCredentials("test", "test");
				awsOptions.Region = Amazon.RegionEndpoint.APSoutheast1; // awsSettings.GetValue<string>("Region")
			}
			services.AddDefaultAWSOptions(awsOptions);

			var client = awsOptions.CreateServiceClient<IAmazonDynamoDB>();
			var tableResponse = client.ListTablesAsync();
			DynamoDbOptions dynamoDbOptions = new DynamoDbOptions();
			ConfigurationBinder.Bind(Configuration.GetSection("DynamoDbTables"), dynamoDbOptions);

			ApplicationService.Load(services, dynamoDbOptions, client);

			if (runLocalDynamoDb)
			{
				services.AddSingleton<IAmazonDynamoDB>(sp =>
				{
					return client;
				});
			}
			else
			{
				services.AddAWSService<IAmazonDynamoDB>();
			}

			//create Initial Table
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
