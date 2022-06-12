using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Api.Application.Features.Queries.GetEntries;

public class GetEntriesQueryHandler : IRequestHandler<GetEnriesQuery, List<GetEntriesViewModel>>
{
    private readonly IEntryRepository _entryRepository;
    private readonly IMapper _mapper;

    public GetEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
    {
        _entryRepository = entryRepository;
        _mapper = mapper;
    }

    public async Task<List<GetEntriesViewModel>> Handle(GetEnriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.GetAsQueryable();

        if (request.TodaysEntries)
        {
            query = query
                .Where(i => i.CreatedDate >= DateTime.Now.Date)
                .Where(i => i.CreatedDate <= DateTime.Now.AddDays(1).Date);
        }

        query = query.Include(i => i.EntryComments)
            .OrderBy(i => Guid.NewGuid()) //Bütün kayitlari siraliyor
            .Take(request.Count); //Icerisinden kac tane istersek o kadar sayfada gözükmesini sagliyoruz

        return await query.ProjectTo<GetEntriesViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}