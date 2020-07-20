using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction.Contracts
{
    public interface ITransactionService
    {
        TransactionExportReadResult GetReadExportResult(IFormFile formFile);
        Task InsertAsync(List<TransactionEntity> transactionsToExport, List<TransactionEntity> exportedDuplicates);
        Task UpdateAsync(List<TransactionEntity> transactionsToExport, List<TransactionEntity> exportedDuplicates);
        Task<List<TransactionEntity>> GetByAsync(IEnumerable<string> transactionIds);
        Task<List<TransactionResponse>> GetByAsync(string currencyCode);
    }
}
