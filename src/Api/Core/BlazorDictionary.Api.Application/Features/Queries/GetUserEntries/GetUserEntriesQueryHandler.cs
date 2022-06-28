using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Infrastructure.Extensions;
using BlazorDictionary.Common.Models.Page;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQueryHandler:IRequestHandler<GetUserEntriesQuery,PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        private readonly IEntryRepository _entryRepository;

        public GetUserEntriesQueryHandler(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryable();

            if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
            {
                query = query.Where(i => i.CreatedById == request.UserId);
            }
            else if (!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(i => i.CreatedBy.UserName == request.UserName);
            }
            else 
                return null;

            query = query.Include(i => i.EntryFavorites)
                         .Include(i => i.CreatedBy);

            var list = query.Select(i => new GetUserEntriesDetailViewModel()
            {
                Id = i.Id,
                Subject = i.Subject,
                Content = i.Content,
                IsFavorited = false,
                FavoritedCount = i.EntryFavorites.Count,
                CreatedDate = i.CreatedDate,
                CreatedByUserName = i.CreatedBy.UserName
            });

            var entries = await list.GetPaged(request.Page, request.PageSize);

            return entries;
        }


    }
}
