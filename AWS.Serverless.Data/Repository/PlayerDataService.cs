using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using AWS.Serverless.Data.Interface;
using AWS.Serverless.DBContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AWS.Serverless.Data.Repository
{
	public class PlayerDataService : IPlayerDataService
	{
		private IDynamoDbContext<Player> _playerContext;
		private IAmazonDynamoDB amazonDynamoDB;

		public PlayerDataService(IDynamoDbContext<Player> playerContext, IAmazonDynamoDB db)
		{
			this.amazonDynamoDB = db;
			_playerContext = playerContext;
		}

		public async Task<bool> CreatePlayer(Player player)
		{
			await _playerContext.SaveAsync(player);
			return true;
		}

		public async Task<Player> GetPlayerAsync(int id)
		{
			try
			{
				return await _playerContext.GetByIdAsync(id);
			}
			catch (Exception ex)
			{
				throw new Exception($"Amazon error in GetUser table operation! Error: {ex}");
			}
		}

		public async Task<List<Player>> GetAllPlayerAsync()
		{
			List<Player> documentList = new List<Player>();
			try
			{
				List<AttributeValue> attr = new List<AttributeValue>();
				attr.Add(new AttributeValue { S = "sumit singh" });
				ScanFilter filter = new ScanFilter();
				filter.AddCondition("Name", ScanOperator.Contains, attr);

				List<string> attributesToGet = new List<string>();
				attributesToGet.Add("Id");

				var expr = new Expression();
				expr.ExpressionStatement = "contains(#Name, :Name) and attribute_not_exists(#FullName) or #FullName = :FullName";
				expr.ExpressionAttributeNames["#FullName"] = "FullName";
				expr.ExpressionAttributeValues[":FullName"] = "sumit singh";

				expr.ExpressionAttributeNames["#Name"] = "Name";
				expr.ExpressionAttributeValues[":Name"] = "sumit singh";
				ScanOperationConfig config = new ScanOperationConfig()


				{
					Limit = 2,
					PaginationToken = "{}",
					//Filter = filter,
					FilterExpression = expr,
						//AttributesToGet = attributesToGet,
						//Select = SelectValues.SpecificAttributes,
					TotalSegments = 1
				};
				var item = _playerContext.FromScanTableAsync(config);
				do
				{
					documentList.AddRange(await item.GetNextSetAsync());

				} while (!item.IsDone);
				return documentList;
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public async Task<bool> DeleteById(int Id)
		{
			await _playerContext.DeleteByIdAsync(Id);
			return true;
		}
	}
}
