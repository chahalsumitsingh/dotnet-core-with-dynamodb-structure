using AWS.Serverless.Data.Interface;
using AWS.Serverless.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWS.Serverless.Logic.Repository
{
	public class TableLogic: ITableLogic
	{
		private ITableDataRepository tableDataRepository;
		public TableLogic(ITableDataRepository tableDataRepository)
		{
			this.tableDataRepository = tableDataRepository;
		}

		public void CreateTable()
		{
			tableDataRepository.CreateTable();
		}
	}
}
