using AutoMapper;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Common.Models.Queries;
using BlazorDictionary.Common.Models.RequestModels;

namespace BlazorDictionary.Api.Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>().ReverseMap();
            
            CreateMap<CreateUserCommand, User>();

            CreateMap<UpdateUserCommand, User>();

            CreateMap<CreateEntryCommand,Entry>().ReverseMap();

            CreateMap<Entry, GetEntriesViewModel>().ForMember(x=>x.CommentCount, y=>y.MapFrom(z=>z.EntryComments.Count));

            CreateMap<CreateEntryCommentCommand, EntryComment>().ReverseMap();
        }
    }
}
