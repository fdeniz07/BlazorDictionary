using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BlazorDictionaryContext dbContext) : base(dbContext)
        {
        }
    }
}
