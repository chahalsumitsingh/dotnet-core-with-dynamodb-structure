using System;
using System.Collections.Generic;
using System.Text;

namespace AWS.Serverless.ViewModel.Models
{
	public class PlayerViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int HitPoints { get; set; }

		public int Gold { get; set; }

		public int Level { get; set; }

		public List<string> Items { get; set; }

	}
}
