using System;
using System.Collections.Generic;
using System.Linq;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.Models.FileReadModels;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction
{
    public class TransactionValidator : ITransactionValidator
    {
        public bool IsDateRangeValid(DateTime dateFrom, DateTime dateTo)
        {
            var isValid = dateTo >= dateFrom;
            return isValid;
        }

        public bool IsStatusValid(string statusInUnifiedFormat)
        {
            return TransactionDefaults
                .TransactionStatusUnifiedFormats
                .Any(x =>
                    x.Value.Equals(statusInUnifiedFormat, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsCurrencyCodeValid(string currencyCode)
        {
            return !string.IsNullOrWhiteSpace(currencyCode) &&
                   currencyCode.Length == TransactionDefaults.IsoCurrencySymbolLength;
        }

        public TransactionReadResult GetValidatedReadResult(List<TransactionModel> transactions)
        {
            var result = new TransactionReadResult();

            if (transactions is null || transactions.Count == default)
            {
                result.ValidationResult.Errors.Add("Transaction list is empty");
                return result;
            }

            transactions.ForEach(Validate);

            if (transactions.Any(x => x.InValid))
                result.ValidationResult.Errors.Add("One or more transactions isn't valid");

            if (result.ValidationResult.HasErrors)
            {
                var invalidRecords = transactions.Where(x => x.InValid);
                result.InvalidTransactionsJson = Newtonsoft.Json.JsonConvert.SerializeObject(invalidRecords);

                return result;
            }

            return result;
        }

        private void Validate(TransactionModel transactionItem)
        {
            var isValid = !string.IsNullOrWhiteSpace(transactionItem.CurrencyCode) &&
                          !string.IsNullOrWhiteSpace(transactionItem.TransactionId) &&
                          transactionItem.TransactionId.Length <= TransactionDefaults.TransactionIdMaxLength &&
                          transactionItem.CurrencyCode.Length == TransactionDefaults.IsoCurrencySymbolLength &&
                          transactionItem.TransactionDate.HasValue &&
                          transactionItem.TransactionDate != default &&
                          transactionItem.Amount.HasValue &&
                          transactionItem.Status.HasValue;

            if (!isValid)
                transactionItem.MarkAsInValid();
        }
    }
}
