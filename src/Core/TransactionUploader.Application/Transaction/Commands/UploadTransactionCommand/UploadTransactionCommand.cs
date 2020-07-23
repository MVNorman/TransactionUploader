using MediatR;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Common;

namespace TransactionUploader.Application.Transaction.Commands.UploadTransactionCommand
{
    public class UploadTransactionCommand: IRequest<ValidationResult>
    {
        public UploadTransactionCommand(IFormFile formFile)
        {
            FormFile = formFile;
        }
        public IFormFile FormFile { get; }
    }
}
