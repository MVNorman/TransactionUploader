using System.Linq;
using TransactionUploader.Domain.EntityRoot;

namespace TransactionUploader.Application.RepositoryRoot
{
    public interface IQueryRepository<out TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> Queryable();
    }
}
