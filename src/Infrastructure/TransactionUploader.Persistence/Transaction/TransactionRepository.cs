using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Domain.Transaction;
using TransactionUploader.Persistence.EFCore;

namespace TransactionUploader.Persistence.Transaction
{
    public class TransactionRepository: ITransactionRepository
    {
        private readonly IEfUnitOfWork<TransactionUploaderDbContext> _efUnitOfWork;

        public TransactionRepository(IEfUnitOfWork<TransactionUploaderDbContext> efUnitOfWork)
        {
            _efUnitOfWork = efUnitOfWork;
        }

        public IQueryable<TransactionEntity> GetQueryable()
        {
            var queryRepository = _efUnitOfWork.QueryRepository<TransactionEntity>();

            return queryRepository.Queryable();
        }

        public async Task UpdateRangeAsync(IEnumerable<TransactionEntity> entities)
        {
            var repository = _efUnitOfWork.Repository<TransactionEntity>();

            repository.UpdateRange(entities);
            await _efUnitOfWork.SaveChangesAsync(default);
        }

        public async Task AddRangeAsync(IEnumerable<TransactionEntity> entities)
        {
            var repository = _efUnitOfWork.Repository<TransactionEntity>();

            await repository.AddRangeAsync(entities);
            await _efUnitOfWork.SaveChangesAsync(default);
        }

        public async Task<List<TransactionEntity>> GetByAsync(IEnumerable<string> transactionIds)
        {
            var queryRepository = _efUnitOfWork.QueryRepository<TransactionEntity>();

            return await queryRepository.Queryable()
                .Where(x => transactionIds.Contains(x.TransactionId))
                .ToListAsync();
        }
    }
}
