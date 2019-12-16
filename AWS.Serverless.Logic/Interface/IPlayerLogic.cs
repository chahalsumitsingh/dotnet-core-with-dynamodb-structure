using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Serverless.Logic.Interface
{
	public interface IPlayerLogic
	{
		bool getAll();
		Task<bool> CreatePlayer();
			
	}
}
