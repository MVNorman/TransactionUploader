using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Enums;
using TransactionUploader.Application.Transaction.Models.FileReadModels;

namespace TransactionUploader.Application.Transaction.FileReadHandlers.Contracts
{
    public interface ITransactionReadHandler
    {
        void SetSuccessor(ITransactionReadHandler successor);
        TransactionReadResult GetReadResult(IFormFile formFile, SupportedFileFormat fileFormat);
    }
}
