using System.Collections.Generic;
using System.Linq;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Common;

namespace TransactionUploader.Application.Transaction
{
    public class TransactionValidator: ITransactionValidator
    {
        private const int IsoCurrencySymbolLength = 3;

        public ValidationResult Validate(List<CsvTransaction> csvTransactions)
        {
            csvTransactions.ForEach(Validate);

            var validationResult = new ValidationResult();

            if (csvTransactions.Any(x => x.InValid))
                validationResult.Errors.Add("One or more transactions isn't valid");

            return validationResult;
        }

        private void Validate(CsvTransaction csvTransaction)
        {
            var isValid = !string.IsNullOrWhiteSpace(csvTransaction.CurrencyCode) &&
                          csvTransaction.CurrencyCode.Length == IsoCurrencySymbolLength &&
                          !string.IsNullOrWhiteSpace(csvTransaction.TransactionId) &&
                          !string.IsNullOrWhiteSpace(csvTransaction.TransactionDate) &&
                          csvTransaction.Amount.HasValue &&
                          csvTransaction.Status.HasValue;

            if(!isValid)
                csvTransaction.MarkAsInValid();
        }
    }
}
