using EP.Cryptocurrency.Repository.Abstractions;
using EP.Cryptocurrency.Storage.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EP.Cryptocurrency.Repository.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task Add(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> Find(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task<IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => _dbContext.Set<TEntity>().Where(predicate)).ConfigureAwait(false);
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            return await Task.Run(() => _dbContext.Set<TEntity>().AsNoTracking()).ConfigureAwait(false);
        }

        public async Task Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
