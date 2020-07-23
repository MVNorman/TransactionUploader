using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction.Contracts
{
    public interface ITransactionRepository
    {
        IQueryable<TransactionEntity> GetQueryable();
        Task UpdateRangeAsync(IEnumerable<TransactionEntity> entities);
        Task AddRangeAsync(IEnumerable<TransactionEntity> entities);
        Task<List<TransactionEntity>> GetByAsync(IEnumerable<string> transactionIds);
    }
}
