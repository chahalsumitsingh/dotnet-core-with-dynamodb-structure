using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWS.Serverless.Logic.Interface;
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
		[Route("getAll")]
		public async Task<bool> getAll()
		{
			var player = playerLogic.getAll();
			return true;
		}

		[HttpGet]
		[Route("createPlayer")]
		public async Task<bool> CreatePlayer()
		{
			var player = playerLogic.CreatePlayer();
			return true;
		}
	}
}