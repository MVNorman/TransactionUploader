using System.Threading.Tasks;
using TransactionUploader.Domain.TransactionLog;

namespace TransactionUploader.Application.TransactionLog.Contracts
{
    public interface ITransactionLogRepository
    {
        Task AddAsync(TransactionLogEntity entity);
    }
}
