using Amazon.DynamoDBv2;
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
			this.checkAndCreateTable();
		}

		public async Task<bool> CreatePlayer()
		{
			Player pl = new Player();
			pl.Name = "sumit singh";
			pl.HitPoints = 20;
			pl.Gold = 2;
			pl.Level = 22;
			pl.Items = new List<Test>();
			pl.Items.Add(new Test() { ItemName= "hey", ItemValue = "44" });
			pl.Items.Add(new Test() { ItemName = "hey 2", ItemValue = "44 55" });
			await _playerContext.SaveAsync(pl);

			return true;
		}

		public async Task<Player> GetPlayerAsync(string id)
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

		private async void checkAndCreateTable()
		{
			var tableResponse = await amazonDynamoDB.ListTablesAsync();

			if (!tableResponse.TableNames.Contains("Player"))
			{
				//Table not found, creating table  
				await amazonDynamoDB.CreateTableAsync(new CreateTableRequest
				{
					TableName = "Player",
					ProvisionedThroughput = new ProvisionedThroughput
					{
						ReadCapacityUnits = 3,
						WriteCapacityUnits = 1
					},
					KeySchema = new List<KeySchemaElement> {
							new KeySchemaElement {
								AttributeName = "Id",
									KeyType = KeyType.HASH
							}
						},
					AttributeDefinitions = new List<AttributeDefinition> {
							new AttributeDefinition {
								AttributeName = "Id",
								AttributeType = ScalarAttributeType.N
							}
						}
				});
				bool isTableAvailable = false;
				while (!isTableAvailable)
				{
					//"Waiting for table to be active...  
					Thread.Sleep(5000);
					var tableStatus = await amazonDynamoDB.DescribeTableAsync("Player");
					isTableAvailable = tableStatus.Table.TableStatus == "ACTIVE";
				}
			}
		}

	}
}
