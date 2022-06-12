using BlazorDictionary.Common.Models.Queries;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Queries.GetEntries
{
    public class GetEnriesQuery:IRequest<List<GetEntriesViewModel>>
    {
        public bool TodaysEntries { get; set; }

        public int Count { get; set; } = 100;
    }
}
