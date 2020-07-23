using System.Linq;
using Microsoft.EntityFrameworkCore;
using TransactionUploader.Application.RepositoryRoot;
using TransactionUploader.Domain.EntityRoot;

namespace TransactionUploader.Persistence.EFCore
{

    public class QueryRepository<TEntity>: IQueryRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _dbContext;

        public QueryRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbContext.Set<TEntity>();
        }
    }
}
