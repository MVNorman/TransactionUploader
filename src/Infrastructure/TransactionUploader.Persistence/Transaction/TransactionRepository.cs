using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Domain.Transaction;
using TransactionUploader.Persistence.RepositoryRoot;

namespace TransactionUploader.Persistence.Transaction
{
    public class TransactionRepository: BaseRepository<TransactionEntity>, ITransactionRepository
    {
        public TransactionRepository(TransactionUploaderDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<TransactionEntity>> GetByAsNoTrackingAsync(string currencyCode)
        {
            return await Queryable()
                .AsNoTracking()
                .Where(x =>
                    x.CurrencyCode.Equals(currencyCode))
                .ToListAsync();
        }

        public async Task<List<TransactionEntity>> GetByAsync(IEnumerable<string> transactionIds)
        {
            return await Queryable()
                .Where(x => transactionIds.Contains(x.TransactionId))
                .ToListAsync();
        }
    }
}
