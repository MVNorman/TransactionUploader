using System;
using System.Threading.Tasks;
using TransactionUploader.Application.TransactionLog.Contracts;
using TransactionUploader.Common;
using TransactionUploader.Domain.TransactionLog;

namespace TransactionUploader.Application.TransactionLog
{
    public class TransactionLogService: ITransactionLogService
    {
        private readonly ITransactionLogRepository _transactionLogRepository;

        public TransactionLogService(ITransactionLogRepository transactionLogRepository)
        {
            _transactionLogRepository = transactionLogRepository;
        }

        public async Task LogErrorAsync(string invalidTransactionsJson)
        {
            var logEntity = new TransactionLogEntity()
            {
                CreatedAt = DateTime.UtcNow,
                LogType = LogType.Error,
                InvalidTransactionsJson = invalidTransactionsJson
            };

            _transactionLogRepository.Insert(logEntity);

            await _transactionLogRepository.SaveChangesAsync();
        }
    }
}
