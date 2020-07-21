using System.Collections.Generic;
using TransactionUploader.Common;

namespace TransactionUploader.Application.Transaction.Models.FileReadModels
{
    public class TransactionReadResult
    {
        public TransactionReadResult()
        {
            ValidationResult = new ValidationResult();
            Transactions = new List<TransactionModel>();
        }

        public string InvalidTransactionsJson { get; set; }
        public ValidationResult ValidationResult { get; }
        public List<TransactionModel> Transactions { get; }
    }
}
