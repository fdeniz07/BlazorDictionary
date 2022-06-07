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
        }
    }
}
