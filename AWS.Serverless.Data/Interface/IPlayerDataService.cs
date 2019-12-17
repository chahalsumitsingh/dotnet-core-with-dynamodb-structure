using AWS.Serverless.DBContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Serverless.Data.Interface
{
	public interface IPlayerDataService
	{
		Task<Player> GetPlayerAsync(int id);
		Task<bool> CreatePlayer(Player player);
		Task<List<Player>> GetAllPlayerAsync();
		Task<bool> DeleteById(int Id);
	}
}
