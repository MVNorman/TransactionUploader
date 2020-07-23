using TransactionUploader.Domain.EntityRoot;

namespace TransactionUploader.Application.RepositoryRoot
{
    public interface IRepositoryFactory
    {
        IQueryRepository<TEntity> QueryRepository<TEntity>() where TEntity : class, IEntity;
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;
    }
}
