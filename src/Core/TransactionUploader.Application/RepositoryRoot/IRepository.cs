using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TransactionUploader.Domain.Entity;

namespace TransactionUploader.Application.RepositoryRoot
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> Queryable();
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        void UpdateRange(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        void Insert(TEntity entity);
        Task InsertRangeAsync(List<TEntity> entities);
    }
}
