using Microsoft.AspNetCore.Http;
using TransactionUploader.Common;

namespace TransactionUploader.Application.FormFile.Contracts
{
    public interface IFormFileValidator
    {
        ValidationResult Validate(IFormFile file, int maxFileSize = 1000000);
    }
}
