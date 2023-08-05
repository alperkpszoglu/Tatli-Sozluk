using AutoMapper;
using SozlukApp.Api.Domain.Models;
using SozlukAppCommon.Models.Queries;
using SozlukAppCommon.Models.RequestModels;

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

            CreateMap<UpdateUserCommand, User>()
                .ReverseMap();

            CreateMap<CreateEntryCommand, Entry>()
                .ReverseMap();

            CreateMap<CreateEntryCommentCommand, EntryComment>()
                .ReverseMap();

            CreateMap<Entry, GetEntriesViewModel>()
                .ForMember(x => x.Count, y => y.MapFrom(i => i.EntryComments.Count));
                
        }
    }
}
