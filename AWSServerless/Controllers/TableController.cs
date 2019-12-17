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
    public class TableController : ControllerBase
    {
		private ITableLogic tableLogic;
		public TableController(ITableLogic tableLogic)
		{
			this.tableLogic = tableLogic;
		}

		[HttpGet]
		[Route("create")]
		public bool CreateTable()
		{
			tableLogic.CreateTable();
			return true;
		}
    }
}