using AutoMapper;
using SozlukApp.Api.Domain.Models;
using SozlukAppCommon.Models.Queries;
using SozlukAppCommon.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Application.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>()
                .ReverseMap();

            CreateMap<CreateUserCommand, User>()
                .ReverseMap();


        }
    }
}
