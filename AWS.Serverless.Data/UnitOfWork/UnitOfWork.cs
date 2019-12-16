using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.DataModel;
using AWS.Serverless.Data.Interface;
using AWS.Serverless.Data.Repository;

namespace AWS.Serverless.Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IDynamoDBContext _context;
		public Lazy<IPlayerDataService> Player { get; private set; }

		public UnitOfWork(IDynamoDBContext context)
		{
			_context = context;
			//Player = new Lazy<IPlayerDataService>(() => new PlayerDataService(context));
		}
	}
}
