using System.Collections.Generic;
using System.Linq;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Application.Transaction.Models.Xml;
using TransactionUploader.Common;

namespace TransactionUploader.Application.Transaction
{
    public class TransactionValidator : ITransactionValidator
    {
        private const int IsoCurrencySymbolLength = 3;

        public ValidationResult Validate(List<CsvTransaction> csvTransactions)
        {
            var validationResult = new ValidationResult();

            if (csvTransactions is null || csvTransactions.Count == default)
            {
                validationResult.Errors.Add("Csv transaction list is empty");
                return validationResult;
            }

            csvTransactions.ForEach(Validate);

            if (csvTransactions.Any(x => x.InValid))
                validationResult.Errors.Add("One or more transactions isn't valid");

            return validationResult;
        }

        public ValidationResult Validate(XmlTransactionRoot xmlTransaction)
        {
            var validationResult = new ValidationResult();

            if (xmlTransaction is null)
            {
                validationResult.Errors.Add("Xml transaction list is empty");
                return validationResult;
            }

            foreach (var transactionItem in xmlTransaction.Transactions)
                Validate(transactionItem);

            if (xmlTransaction.Transactions.Any(x => x.InValid))
                validationResult.Errors.Add("One or more transactions isn't valid");

            return validationResult;
        }

        private void Validate(XmlTransactionItem transactionItem)
        {
            var isValid = transactionItem.PaymentDetails != null &&
                          !string.IsNullOrWhiteSpace(transactionItem.PaymentDetails.CurrencyCode) &&
                          transactionItem.PaymentDetails.CurrencyCode.Length == IsoCurrencySymbolLength &&
                          !string.IsNullOrWhiteSpace(transactionItem.TransactionId) &&
                          !string.IsNullOrWhiteSpace(transactionItem.TransactionDate) &&
                          transactionItem.PaymentDetails.Amount.HasValue &&
                          transactionItem.Status.HasValue;

            if (!isValid)
                transactionItem.MarkAsInValid();
        }

        private void Validate(CsvTransaction csvTransaction)
        {
            var isValid = !string.IsNullOrWhiteSpace(csvTransaction.CurrencyCode) &&
                          csvTransaction.CurrencyCode.Length == IsoCurrencySymbolLength &&
                          !string.IsNullOrWhiteSpace(csvTransaction.TransactionId) &&
                          !string.IsNullOrWhiteSpace(csvTransaction.TransactionDate) &&
                          csvTransaction.Amount.HasValue &&
                          csvTransaction.Status.HasValue;

            if (!isValid)
                csvTransaction.MarkAsInValid();
        }
    }
}
