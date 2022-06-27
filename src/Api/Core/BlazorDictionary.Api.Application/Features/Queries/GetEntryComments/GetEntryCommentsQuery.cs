using BlazorDictionary.Common.Models.Page;

namespace BlazorDictionary.Api.Application.Features.Queries.GetEntryComments
{
    public class GetEntryCommentsQuery:BasePagedQuery
    {
        public GetEntryCommentsQuery(int page, int pageSize) : base(page, pageSize)
        {
        }

        public Guid EntryId { get; set; }

        public Guid UserId { get; set; }
    }
}
