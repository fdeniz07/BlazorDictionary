using BlazorDictionary.Api.Domain.Models;

namespace BlazorDictionary.Api.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where  TEntity : BaseEntity
    {
        Task<int> AddAsync(TEntity entity);

        Task<int> UpdateAsync(TEntity entity);

        Task<int> DeleteAsync(Guid id);
    }
}
