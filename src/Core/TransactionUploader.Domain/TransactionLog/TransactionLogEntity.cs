using System;
using TransactionUploader.Common;
using TransactionUploader.Domain.Entity;

namespace TransactionUploader.Domain.TransactionLog
{
    public class TransactionLogEntity: IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public LogType LogType { get; set; }
        public string InvalidTransactionsJson { get; set; }
    }
}
