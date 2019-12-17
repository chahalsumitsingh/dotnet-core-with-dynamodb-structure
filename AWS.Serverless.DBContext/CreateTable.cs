using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AWS.Serverless.DBContext
{
	public class CreateTable
	{
		private IAmazonDynamoDB amazonDynamoDB;
		public CreateTable(IAmazonDynamoDB db)
		{
			amazonDynamoDB = db;
			checkAndCreateTable();
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
