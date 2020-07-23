using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Application.Transaction.Models.FileReadModels;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction.Contracts
{
    public interface ITransactionService
    {
        TransactionReadResult GetReadExportResult(IFormFile formFile);
        Task InsertAsync(List<TransactionModel> transactionsToExport, List<TransactionEntity> exportedDuplicates);
        Task UpdateAsync(List<TransactionModel> transactionsToExport, List<TransactionEntity> exportedDuplicates);
        Task<List<TransactionEntity>> GetByAsync(IEnumerable<string> transactionIds);

        Task<List<TransactionResponse>> GetByCurrencyAsync(string currencyCode);
        Task<List<TransactionResponse>> GetByStatusAsync(string statusInUnifiedFormat);
        Task<List<TransactionResponse>> GetByDateRangeAsync(DateTime dateFrom, DateTime dateTo);
    }
}
