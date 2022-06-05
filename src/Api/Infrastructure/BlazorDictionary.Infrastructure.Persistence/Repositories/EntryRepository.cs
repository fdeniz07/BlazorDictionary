using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Infrastructure.Persistence.Repositories
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(BlazorDictionaryContext dbContext) : base(dbContext)
        {
        }
    }
}
