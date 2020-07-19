using System.Collections.Generic;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Common;

namespace TransactionUploader.Application.Transaction
{
    public interface ITransactionValidator
    {
        ValidationResult Validate(List<CsvTransaction> csvTransactions);
    }
}
