using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionUploader.Domain.EntityRoot;

namespace TransactionUploader.Application.RepositoryRoot
{
    public interface ICommandRepository<in TEntity> where TEntity : IEntity
    {
        void UpdateRange(IEnumerable<TEntity> entities);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }
}
