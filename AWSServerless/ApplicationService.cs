using Amazon.DynamoDBv2;
using AWS.Serverless.Data.Interface;
using AWS.Serverless.Data.Repository;
using AWS.Serverless.Data.UnitOfWork;
using AWS.Serverless.DBContext;
using AWS.Serverless.Logic.Interface;
using AWS.Serverless.Logic.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerless
{
	public class ApplicationService
	{
		public static void Load(IServiceCollection services, DynamoDbOptions dynamoDbOptions, IAmazonDynamoDB client)
		{
			services.AddScoped<IPlayerLogic, PlayerLogic>();
			//services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IPlayerDataService, PlayerDataService>();
			services.AddScoped<IDynamoDbContext<Player>>(provider => new DynamoDbContext<Player>(client, dynamoDbOptions.Player));
			services.AddScoped<IDynamoDbContext<Location>>(provider => new DynamoDbContext<Location>(client, dynamoDbOptions.Location));
		}
	}
}
