﻿using TransactionUploader.Application.TransactionLog;
using TransactionUploader.Domain.TransactionLog;
using TransactionUploader.Persistence.RepositoryRoot;

namespace TransactionUploader.Persistence.TransactionLog
{
    public class TransactionLogRepository: BaseRepository<TransactionLogEntity>, ITransactionLogRepository
    {
        public TransactionLogRepository(TransactionUploaderDbContext dbContext) : base(dbContext)
        {
        }
    }
}