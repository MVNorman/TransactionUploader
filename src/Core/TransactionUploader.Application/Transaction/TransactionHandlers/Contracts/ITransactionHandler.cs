using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Enums;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Application.Transaction.Models.FileReadModels;

namespace TransactionUploader.Application.Transaction.TransactionHandlers.Contracts
{
    public interface ITransactionHandler
    {
        void SetSuccessor(ITransactionHandler successor);
        TransactionReadResult GetReadResult(IFormFile formFile, FileFormat fileFormat);
    }
}
