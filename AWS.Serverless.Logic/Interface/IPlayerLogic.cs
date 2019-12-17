using AWS.Serverless.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Serverless.Logic.Interface
{
	public interface IPlayerLogic
	{
		Task<PlayerViewModel> getById(int Id);
		Task<bool> CreatePlayer(PlayerViewModel playerViewModel);
		Task<List<PlayerViewModel>> GetAllPlayer();
		Task<bool> DeteteById(int Id);
	}
}
