using AutoMapper;
using AWS.Serverless.DBContext;
using AWS.Serverless.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWS.Serverless.Common.Mapper
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<PlayerViewModel, Player>();
			CreateMap<Player, PlayerViewModel>();
		}
	}
}
