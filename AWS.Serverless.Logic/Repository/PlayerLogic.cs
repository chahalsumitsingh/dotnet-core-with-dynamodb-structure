using AWS.Serverless.Data.Interface;
using AWS.Serverless.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Serverless.Logic.Repository
{
	public class PlayerLogic : IPlayerLogic
	{
		public IPlayerDataService playerDataService { get; }
		public PlayerLogic(IPlayerDataService playerDataService)
		{
			this.playerDataService = playerDataService;
		}

		public bool getAll()
		{
			playerDataService.GetPlayerAsync("22");
			return true;
		}

		public async Task<bool> CreatePlayer()
		{
			var isSuccess =await playerDataService.CreatePlayer();

			//throw new NotImplementedException();
			return isSuccess;
		}
	}
}
