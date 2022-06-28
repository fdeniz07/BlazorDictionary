using BlazorDictionary.Common.Models.Page;
using BlazorDictionary.Common.Models.Queries;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQuery : BasePagedQuery, IRequest<PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        public Guid? UserId { get; set; }

        public  string UserName { get; set; }


        public GetUserEntriesQuery(Guid? userId, string userName=null, int page=1, int pageSize=10) : base(page, pageSize)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}
