using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Infrastructure.Extensions;
using BlazorDictionary.Common.Models;
using BlazorDictionary.Common.Models.Page;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Api.Application.Features.Queries.GetMainPageEntries;

public class
    GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery,
        PagedViewModel<GetEntryDetailViewModel>>
{
    private readonly IEntryRepository _entryRepository;


    public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;

    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        query = query.Include(i => i.EntryFavorites)
                     .Include(i => i.CreatedBy)
                     .Include(i => i.EntryVotes);

        var list = query.Select(i => new GetEntryDetailViewModel()
        {
            Id = i.Id,
            Subject = i.Subject,
            Content = i.Content,
            IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.CreatedById == request.UserId),
            FavoritedCount = i.EntryFavorites.Count,
            CreatedDate = i.CreatedDate,
            CreatedByUserName = i.CreatedBy.UserName,
            VoteType = request.UserId.HasValue && i.EntryVotes.Any(j => j.CreatedById == request.UserId) ? i.EntryVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType : VoteType.None
        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}