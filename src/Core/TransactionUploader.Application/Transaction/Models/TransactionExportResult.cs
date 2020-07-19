using System.Collections.Generic;
using TransactionUploader.Common;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction.Models
{
    public class TransactionExportResult
    {
        public TransactionExportResult()
        {
            ValidationResult = new ValidationResult();
            Transactions = new List<TransactionEntity>();
        }

        public ValidationResult ValidationResult { get; set; }
        public List<TransactionEntity> Transactions { get; }
    }
}
