using AutoMapper;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Common.Models.Queries;

namespace BlazorDictionary.Api.Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>().ReverseMap();

        }
    }
}
