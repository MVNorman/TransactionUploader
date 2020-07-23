using System.Threading.Tasks;
using TransactionUploader.Application.TransactionLog.Contracts;
using TransactionUploader.Domain.TransactionLog;
using TransactionUploader.Persistence.EFCore;

namespace TransactionUploader.Persistence.TransactionLog
{
    public class TransactionLogRepository: ITransactionLogRepository
    {
        private readonly IEfUnitOfWork<TransactionUploaderDbContext> _efUnitOfWork;

        public TransactionLogRepository(IEfUnitOfWork<TransactionUploaderDbContext> efUnitOfWork)
        {
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task AddAsync(TransactionLogEntity entity)
        {
            var repository = _efUnitOfWork.CommandRepository<TransactionLogEntity>();

            await repository.AddAsync(entity);
            await _efUnitOfWork.SaveChangesAsync(default);
        }
    }
}
