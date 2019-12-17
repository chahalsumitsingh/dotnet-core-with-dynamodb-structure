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
			try
			{
				ScanOperationConfig config = new ScanOperationConfig()
				{
					Limit = 2,
					PaginationToken = "{}"
				};
				List<Player> documentList =await _playerContext.FromScanTableAsync(config);
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
