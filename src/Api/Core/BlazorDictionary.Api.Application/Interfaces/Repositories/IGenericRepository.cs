using System.Linq.Expressions;
using BlazorDictionary.Api.Domain.Models;

namespace BlazorDictionary.Api.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        #region Insert Methods


        Task<int> AddAsync(TEntity entity);

        int Add(TEntity entity);

        int Add(IEnumerable<TEntity> entities);

        Task<int> AddAsync(IEnumerable<TEntity> entities);


        #endregion




        #region Update Methods


        Task<int> UpdateAsync(TEntity entity);

        int Update(TEntity entity);


        #endregion




        #region Delete Methods


        Task<int> DeleteAsync(TEntity entity);

        int Delete(TEntity entity);

        Task<int> DeleteAsync(Guid id);

        int Delete(Guid id);

        bool DeleteRange(Expression<Func<TEntity, bool>> predicate);

        Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate);


        #endregion


        #region AddOrUpdate Methods


        Task<int> AddOrUpdateAsync(TEntity entity);

        int AddOrUpdate(TEntity entity);


        #endregion



       
        #region Get Methods
        
        Task<List<TEntity>> GetAll(bool noTracking = true);

        Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetAsyncV2(IList<Expression<Func<TEntity, bool>>> predicates, IList<Expression<Func<TEntity, object>>> includeProperties);

        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IList<TEntity>> GetAllAsyncV2(IList<Expression<Func<TEntity, bool>>> predicates, IList<Expression<Func<TEntity, object>>> includeProperties);

        Task<TEntity> GetByIdAsync(int id);

        IQueryable<TEntity> AsQueryable();

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate); //Böyle bir entity daha önceden var mi diye kontrol ediyoruz 
        
        IQueryable<TEntity> GetAsQueryable(); // Iqueryable olarak verilen enitity'i, bizlere return eder. Bu sayede bizler, herhangi bir sinir olmadan kompleks sorgular olusturabiliyoruz. Farzedelim, GetAsync icerisinde kategoriyi alirken,onun makalelerini include etmek istiyoruz ve makale icerisindeki yorumlari da onun sonrasinda ThenInclude ile include etmek istiyoruz. Burada komplex bir sorgu ile karsilasiyoruz. Bunu normal metotlarimiz icerisinde tamamlayamiyoruz.

        Task<IList<TEntity>> SearchAsync(IList<Expression<Func<TEntity, bool>>> predicates, params Expression<Func<TEntity, object>>[] includeProperties); //Ayni anda birden fazla arama kriteri istenilebilir. Aradigimiz makalelerin, kategori,yorum,kullanicilari ile gelmelerini isteyecegimizden params kullaniyoruz.

        

        Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true,
            params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true,
            params Expression<Func<TEntity, object>>[] includes);

        //Tüm entity lerin sayisini dönmek icin de Count kullaniyoruz (var commentCount = _commentRepository.CountAsync()), olurda tablodaki bilgileri dönmek istersek, predicate alanina varsayilan deger olarak null atiyoruz.
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);


        #endregion






        #region Bulk Methods


        Task BulkDeleteById(IEnumerable<Guid> ids);

        Task BulkDelete(Expression<Func<TEntity, bool>> predicate);

        Task BulkDelete(IEnumerable<TEntity> entities);

        Task BulkUpdate(IEnumerable<TEntity> entities);

        Task BulkAdd(IEnumerable<TEntity> entities);


        #endregion


    }


}
