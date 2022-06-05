using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Api.Domain.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using BlazorDictionary.Infrastructure.Persistence.Context;

namespace BlazorDictionary.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet; //protected DbSet<TEntity> _dbSet => _dbContext.Set<TEntity>();


        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }


        #region Implementation of IGenericRepository<TEntity>


        #region Notes

        //AsNoTracking metodu bizim icin gereksiz include islemlerini önler veya sadece bizim istedigimiz include ler varsa getirir. Bu sayede gereksiz yere ayni islemler,birbirini tekrar eden islemler (Entry -> EntryComment -> EntryVote -> EntryFavorite -> Entry) gerceklesmeyecegi icin epey bir performans saglamis olacagiz.

        //Kullandigimiz metotlari virtual olarak isaretlenmesinin nedeni, istersek kullanacagimiz repository'ler icierisinde özellestirebiliriz demek icin.

        #endregion


        #region Insert Methods


        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }


        public virtual int Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return _dbContext.SaveChanges();
        }


        public virtual int Add(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any())
                return 0;

            _dbSet.AddRange(_dbSet);
            return _dbContext.SaveChanges();
        }


        public virtual async Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any())
                return 0;

            await _dbSet.AddRangeAsync(entities);
            return await _dbContext.SaveChangesAsync();
        }


        #endregion


        #region Update Methods


        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync();
        }


        public virtual int Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            return _dbContext.SaveChanges();
        }

        #endregion


        #region Delete Methods


        public virtual Task<int> DeleteAsync(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);

            return _dbContext.SaveChangesAsync();
        }


        public virtual int Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);

            return _dbContext.SaveChanges();
        }


        public virtual Task<int> DeleteAsync(Guid id)
        {
            var entity = _dbSet.Find(id);
            return DeleteAsync(entity);
        }


        public virtual int Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            return Delete(entity);
        }


        public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            _dbContext.RemoveRange(_dbSet.Where(predicate));
            return _dbContext.SaveChanges() > 0;
        }


        public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            _dbContext.RemoveRange(_dbSet.Where(predicate));
            return await _dbContext.SaveChangesAsync() > 0;
        }


        #endregion


        #region AddOrUpdate Methods


        public virtual Task<int> AddOrUpdateAsync(TEntity entity)
        {
            // check the entity with the id already tracked
            if (_dbSet.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                _dbContext.Update(entity);

            return _dbContext.SaveChangesAsync();
        }


        public virtual int AddOrUpdate(TEntity entity)
        {
            if (_dbSet.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                _dbContext.Update(entity);

            return _dbContext.SaveChanges();
        }


        #endregion


        #region Get Methods


        public IQueryable<TEntity> AsQueryable() => _dbSet.AsQueryable();


        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.Where(predicate);

            return query;
        }


        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            return Get(predicate, noTracking, includes).FirstOrDefaultAsync();
        }


        public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            if (orderBy != null)
                query = orderBy(query);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();

        }


        public virtual async Task<List<TEntity>> GetAll(bool noTracking = true)
        {
            if (noTracking)
                return await _dbSet.AsNoTracking().ToListAsync();

            return await _dbSet.ToListAsync();
        }


        public virtual async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            TEntity found = await _dbSet.FindAsync(id);

            if (found == null)
                return null;

            if (noTracking)
                _dbContext.Entry(found).State = EntityState.Detached;

            foreach (Expression<Func<TEntity, object>> include in includes)
                _dbContext.Entry(found).Reference(include).Load(); //Lazy loading olarak degeri geri dönüyoruz

            return found;

        }


        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();
        }


        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }


        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await (predicate == null ? _dbSet.CountAsync() : _dbSet.CountAsync(predicate)); //predicate null gelirse, o zaman context e set edilmis olan tenitity nin countasync tamamiyle dönüyoruz. Null gelmezse, gelen predicate degeri filtreleme yaparak kullaniciya dönecegiz. Örnek: Entry tablosunda 6 kayit varsa, toplam 6 degerini predicatesiz olarak dönecegiz. Fakat olur da silinmis Entry leri görmek istersek ve tablomuzda 3 Entry silinmisse; o zaman predicate ile toplam 3 Entry degerini kullaniciya dönüyoruz. Esnek bir yapi kurmus oluyoruz
        }


        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.AsNoTracking().SingleOrDefaultAsync();
        }


        public virtual async Task<TEntity> GetAsyncV2(IList<Expression<Func<TEntity, bool>>> predicates, IList<Expression<Func<TEntity, object>>> includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            if (predicates != null && predicates.Any()) //buraya yanlislikla bos bir liste gönderme ihtimalimiz var. O yüzden null olma durumunu ve listenin icerisinde verinin varligini kontrol ediyoruz
            {
                foreach (var predicate in predicates)
                {
                    query = query.Where(predicate); // isActive==false && isDeleted==true gelebilir
                }
            }

            if (includeProperties != null && includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().SingleOrDefaultAsync();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any()) //bu dizinin icerisinde bir deger varsa, icerisinde döngü ile dönecegiz
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.AsNoTracking().ToListAsync(); //yukarida dönen degerleri kullanicaya bir liste olarak dönecegiz.
        }

        public virtual async Task<IList<TEntity>> GetAllAsyncV2(IList<Expression<Func<TEntity, bool>>> predicates, IList<Expression<Func<TEntity, object>>> includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            if (predicates != null && predicates.Any()) //buraya yanlislikla bos bir liste gönderme ihtimalimiz var. O yüzden null olma durumunu ve listenin icerisinde verinin varligini kontrol ediyoruz
            {
                foreach (var predicate in predicates)
                {
                    query = query.Where(predicate); // isActive==false && isDeleted==true gelebilir
                }
            }

            if (includeProperties != null && includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.AsNoTracking().ToListAsync();
        }


        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }


        //Ayni anda birden fazla arama kriteri istenilebilir. Aradigimiz entry lerin, kategori,yorum,kullanicilari ile gelmelerini isteyecegimizden params kullaniyoruz.
        public virtual async Task<IList<TEntity>> SearchAsync(IList<Expression<Func<TEntity, bool>>> predicates, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            if (predicates.Any())
            {
                var predicateChain = PredicateBuilder.New<TEntity>();
                foreach (var predicate in predicates)
                {
                    //query.Where(predicate) predicate1 && predicate2 && predicate3 && predicateN ve operatörü ile calisir. Bize veya ile ilgili detayli sorgulama islemleri gerektigi icin bir nugetpaket kurmamiz gerekiyor.LinqKit.Microsoft.EntityFrameworkCore isimli paketi kuruyoruz.
                    //query = query.Where(predicate);

                    //predicateChain.Or(predicate) predicate1 || predicate2 || predicate3 || predicateN
                    predicateChain.Or(predicate);
                }

                query = query.Where(predicateChain);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().ToListAsync();
        }


        public virtual IQueryable<TEntity> GetAsQueryable()
        {
            return _dbSet.AsQueryable(); // UnitOfWork.Entry.GetAsQueryable(); dedigimizde bize entry nesnesini bir Querable nesnesi olarak return ediyor.
        }


        #endregion


        #region Bulk Methods

        public virtual Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            if (ids != null && !ids.Any()) //id'ler bossa ya da yoksa görevi tamamliyoruz
                return Task.CompletedTask;

            _dbContext.RemoveRange(_dbSet.Where(i => ids.Contains(i.Id))); //basgli olan tüm tablolari siliyoruz
            return _dbContext.SaveChangesAsync();
        }


        public virtual Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            _dbContext.RemoveRange(_dbSet.Where(predicate));
            return _dbContext.SaveChangesAsync();
        }


        public Task BulkDelete(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any())
                return Task.CompletedTask;

            _dbSet.RemoveRange(entities);
            return _dbContext.SaveChangesAsync();
        }


        public virtual Task BulkUpdate(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any())
                return Task.CompletedTask;


            // _dbSet.UpdateRange(entities); foreach yerine kisaca böylede yapabiliriz
            foreach (var entityItem in entities)
            {
                _dbSet.Update(entityItem);
               
            }

            return _dbContext.SaveChangesAsync();
        }


        public virtual async Task BulkAdd(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any())
                await Task.CompletedTask;

            await _dbSet.AddRangeAsync(entities);

            await _dbContext.SaveChangesAsync();
        }


        #endregion


        #region SaveChanges Methods

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        #endregion

        
        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
            {
                foreach (var includeItem in includes)
                {
                    query = query.Include(includeItem);
                }
            }

            return query;
        }

        #endregion
    }
}
