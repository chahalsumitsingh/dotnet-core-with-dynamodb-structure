using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Serverless.DBContext
{
	public interface IDynamoDbContext<T>: IDisposable where T : class
	{
		Task<T> GetByIdAsync(string id);
		Task SaveAsync(T item);
		Task DeleteByIdAsync(T item);
	}
}
