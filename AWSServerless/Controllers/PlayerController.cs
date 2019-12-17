using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWS.Serverless.Logic.Interface;
using AWS.Serverless.ViewModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWSServerless.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
	{
		public IPlayerLogic playerLogic;

		public PlayerController(IPlayerLogic playerLogic)
		{
			this.playerLogic = playerLogic;
		}

		[HttpGet]
		[Route("getById/{Id}")]
		public async Task<PlayerViewModel> getById(int Id)
		{
			PlayerViewModel player =await playerLogic.getById(Id);
			return player;
		}

		[HttpPost]
		[Route("createPlayer")]
		public async Task<bool> CreatePlayer([FromBody]PlayerViewModel playerViewModel)
		{
			bool isSuccess =await playerLogic.CreatePlayer(playerViewModel);
			return isSuccess;
		}

		[HttpGet]
		[Route("getAll")]
		public async Task<List<PlayerViewModel>> GetAppPlayer()
		{
			return await playerLogic.GetAllPlayer();
		}

		[HttpDelete]
		[Route("deleteById/{Id}")]
		public async Task<bool> DeteteById(int Id)
		{
			return await playerLogic.DeteteById(Id);
		}
	}
}