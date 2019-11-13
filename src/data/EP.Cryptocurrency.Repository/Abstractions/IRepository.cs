using EP.Cryptocurrency.Storage.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EP.Cryptocurrency.Repository.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task Add(TEntity entity);

        Task Delete(TEntity entity);

        Task<TEntity> Find(int id);

        Task<IQueryable<TEntity>> GetAll();

        Task<IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);

        Task Update(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entities);
        Task UpdateRange(IEnumerable<TEntity> entities);

    }
}
