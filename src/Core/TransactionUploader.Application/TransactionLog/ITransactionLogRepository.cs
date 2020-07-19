using TransactionUploader.Application.RepositoryRoot;
using TransactionUploader.Domain.TransactionLog;

namespace TransactionUploader.Application.TransactionLog
{
    public interface ITransactionLogRepository: IRepository<TransactionLogEntity>
    {
    }
}
