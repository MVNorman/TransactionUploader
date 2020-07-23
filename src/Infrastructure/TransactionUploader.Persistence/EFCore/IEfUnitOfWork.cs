using Microsoft.EntityFrameworkCore;
using TransactionUploader.Application.UnitOfWork;

namespace TransactionUploader.Persistence.EFCore
{
    public interface IEfUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {

    }
}
