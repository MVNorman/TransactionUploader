using TransactionUploader.Application.RepositoryRoot;
using TransactionUploader.Domain.TransactionLog;

namespace TransactionUploader.Application.TransactionLog.Contracts
{
    public interface ITransactionLogRepository: IRepository<TransactionLogEntity>
    {
    }
}
