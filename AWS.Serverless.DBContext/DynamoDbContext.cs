using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Serverless.DBContext
{
	public class DynamoDbContext<T> : DynamoDBContext, IDynamoDbContext<T> where T : class
	{
		private DynamoDBOperationConfig _config;

		public DynamoDbContext(IAmazonDynamoDB client, string tableName)
			: base(client)
		{
			_config = new DynamoDBOperationConfig()
			{
				OverrideTableName = tableName
			};
		}

		public async Task<T> GetByIdAsync(string id)
		{
			return await base.LoadAsync<T>(id, _config);
		}

		public async Task SaveAsync(T item)
		{
			await base.SaveAsync(item, _config);
		}

		public async Task DeleteByIdAsync(T item)
		{
			await base.DeleteAsync(item, _config);
		}

	}
}

