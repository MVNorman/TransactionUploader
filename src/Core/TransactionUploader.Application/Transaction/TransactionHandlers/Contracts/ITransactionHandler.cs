using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Enums;
using TransactionUploader.Application.Transaction.Models;

namespace TransactionUploader.Application.Transaction.TransactionHandlers.Contracts
{
    public interface ITransactionHandler
    {
        void SetSuccessor(ITransactionHandler successor);
        TransactionExportReadResult GetTransactionReadResult(IFormFile formFile, FileFormat fileFormat);
    }
}
