using System;
using System.Threading;
using System.Threading.Tasks;
using TransactionUploader.Application.RepositoryRoot;

namespace TransactionUploader.Application.UnitOfWork
{
    public interface IUnitOfWork : IRepositoryFactory, IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
