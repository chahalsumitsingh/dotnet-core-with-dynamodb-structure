using AutoMapper;
using AWS.Serverless.Data.Interface;
using AWS.Serverless.DBContext;
using AWS.Serverless.Logic.Interface;
using AWS.Serverless.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Serverless.Logic.Repository
{
	public class PlayerLogic : IPlayerLogic
	{
		private readonly IMapper mapper;
		public IPlayerDataService playerDataService { get; }
		public PlayerLogic(IPlayerDataService playerDataService, IMapper mapper)
		{
			this.playerDataService = playerDataService;
			this.mapper = mapper;
		}

		public async Task<PlayerViewModel> getById(int id)
		{
			Player player =await playerDataService.GetPlayerAsync(id);
			return mapper.Map<PlayerViewModel>(player);
		}

		public async Task<bool> CreatePlayer(PlayerViewModel playerViewModel)
		{
			Player player = mapper.Map<Player>(playerViewModel);
			bool isSuccess =await playerDataService.CreatePlayer(player);
			return isSuccess;
		}

		public async Task<List<PlayerViewModel>> GetAllPlayer()
		{
			return mapper.Map<List<PlayerViewModel>>(await playerDataService.GetAllPlayerAsync());
		}

		public async Task<bool> DeteteById(int Id)
		{
			return await playerDataService.DeleteById(Id);
		}
	}
}
