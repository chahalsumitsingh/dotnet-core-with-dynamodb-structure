using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWS.Serverless.DBContext
{
	public class Location
	{
		[DynamoDBHashKey]
		public int Id { get; set; }

		[DynamoDBProperty]
		public string Name { get; set; }

		[DynamoDBProperty]
		public string Description { get; set; }
	}
}
