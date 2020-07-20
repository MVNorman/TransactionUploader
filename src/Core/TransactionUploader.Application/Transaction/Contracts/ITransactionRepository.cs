using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionUploader.Application.RepositoryRoot;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction.Contracts
{
    public interface ITransactionRepository: IRepository<TransactionEntity>
    {
        Task<List<TransactionEntity>> GetByAsNoTrackingAsync(string currencyCode);
        Task<List<TransactionEntity>> GetByAsync(IEnumerable<string> transactionIds);
    }

}
