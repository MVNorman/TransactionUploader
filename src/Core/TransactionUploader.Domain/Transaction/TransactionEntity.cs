using System;
using TransactionUploader.Domain.Entity;
using TransactionUploader.Domain.Transaction.Enums;

namespace TransactionUploader.Domain.Transaction
{
    public class TransactionEntity: IEntity
    {
        public Guid Id { get; set; }

        public string TransactionId { get; set; }
        public string CurrencyCode { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
