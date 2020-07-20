using System.Collections.Generic;
using TransactionUploader.Common;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction.Models
{
    public class TransactionExportReadResult
    {
        public TransactionExportReadResult()
        {
            ValidationResult = new ValidationResult();
            TransactionsToExport = new List<TransactionEntity>();
        }

        public string InvalidTransactionsJson { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public List<TransactionEntity> TransactionsToExport { get; }
    }
}
