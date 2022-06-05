using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Infrastructure.Persistence.Repositories
{
    public class EntryCommentRepository : GenericRepository<EntryComment>, IEntryCommentRepository
    {
        public EntryCommentRepository(BlazorDictionaryContext dbContext) : base(dbContext)
        {
        }
    }
}
