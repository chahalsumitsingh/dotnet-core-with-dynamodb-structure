using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Serverless.DBContext
{
	public interface IDynamoDbContext<T>: IDisposable where T : class
	{
		Task<T> GetByIdAsync(string id);
		Task<T> GetByIdAsync(int id);
		Task SaveAsync(T item);
		Task DeleteByIdAsync(T item);
		Task DeleteByIdAsync(object item);
		AsyncSearch<T> FromScanTableAsync(ScanOperationConfig scanOperationConfig);

	}
}
