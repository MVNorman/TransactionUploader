using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransactionUploader.Application.RepositoryRoot;
using TransactionUploader.Domain.EntityRoot;

namespace TransactionUploader.Persistence.EFCore
{
    public class EfUnitOfWork<TDbContext> : IEfUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private ConcurrentDictionary<string, object> _repositories;

        public EfUnitOfWork(TDbContext context)
        {
            _context = context;
        }

        public IQueryRepository<TEntity> QueryRepository<TEntity>() where TEntity : class, IEntity
        {
            if (_repositories == null)
                _repositories = new ConcurrentDictionary<string, object>();

            var key = $"{typeof(TEntity)}-QUERY";
            if (!_repositories.ContainsKey(key))
            {
                var cachedRepository = new QueryRepository<TEntity>(_context);
                _repositories[key] = cachedRepository;
            }

            return (IQueryRepository<TEntity>)_repositories[key];
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
        {
            if (_repositories == null) _repositories = new ConcurrentDictionary<string, object>();

            var key = $"{typeof(TEntity)}-COMMAND";
            if (!_repositories.ContainsKey(key))
                _repositories[key] = new Repository<TEntity>(_context);

            return (IRepository<TEntity>)_repositories[key];
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
