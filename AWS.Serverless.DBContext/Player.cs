using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWS.Serverless.DBContext
{
	public class Player
	{
		[DynamoDBHashKey]
		public int Id { get; set; }

		[DynamoDBProperty]
		public string Name { get; set; }

		[DynamoDBProperty]
		public int HitPoints { get; set; }

		[DynamoDBProperty]
		public int Gold { get; set; }

		[DynamoDBProperty]
		public int Level { get; set; }

		[DynamoDBProperty]
		public List<string> Items { get; set; }
	}

	//public class Test
	//{
	//	public string ItemName { get; set; }

	//	public string ItemValue { get; set; }
	//}
}
