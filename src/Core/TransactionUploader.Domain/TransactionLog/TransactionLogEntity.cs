using System;
using TransactionUploader.Domain.Entity;
using TransactionUploader.Domain.Transaction.Enums;

namespace TransactionUploader.Domain.TransactionLog
{
    public class TransactionLogEntity: IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string InvalidTransactionsJson { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
