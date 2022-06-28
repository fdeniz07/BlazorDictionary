using BlazorDictionary.Common.Models.Queries;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Queries.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<UserDetailViewModel>
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public GetUserDetailQuery(Guid userId, string userName=null)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}
