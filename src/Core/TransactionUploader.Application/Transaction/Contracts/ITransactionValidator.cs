using System;
using System.Collections.Generic;
using TransactionUploader.Application.Transaction.Models.FileReadModels;

namespace TransactionUploader.Application.Transaction.Contracts
{
    public interface ITransactionValidator
    {
        bool IsDateRangeValid(DateTime dateFrom, DateTime dateTo);
        bool IsStatusValid(string statusInUnifiedFormat);
        bool IsCurrencyCodeValid(string currencyCode);
        TransactionReadResult GetValidatedReadResult(List<TransactionModel> transactions);
    }
}
