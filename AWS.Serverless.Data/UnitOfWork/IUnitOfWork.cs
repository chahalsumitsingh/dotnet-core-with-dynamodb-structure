using AWS.Serverless.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWS.Serverless.Data.UnitOfWork
{
	public interface IUnitOfWork
	{
		Lazy<IPlayerDataService> Player { get; }
	}
}
