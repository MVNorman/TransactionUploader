using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Contracts;
using TransactionUploader.Application.FormFile.Enums;
using TransactionUploader.Common;

namespace TransactionUploader.Application.FormFile
{
    public class FormFileValidator: IFormFileValidator
    {
        public ValidationResult Validate(IFormFile file, decimal maxFileSizeInBytes = 1048576)
        {
            var validationResult = new ValidationResult();

            if (file is null)
            {
                validationResult.Errors.Add("File doesn't exist");
                return validationResult;
            }

            if (file.Length == default)
                validationResult.Errors.Add("File can not be empty");

            var fileFormat = file.GetFileFormat();
            if (fileFormat == SupportedFileFormat.Unknown)
                validationResult.Errors.Add("Unknown format");

            if (file.Length > maxFileSizeInBytes)
            {
                var megabytes = (maxFileSizeInBytes / 1024) / 1024;
                validationResult.Errors.Add($"Max file size is {megabytes} MB");
            }

            return validationResult;
        }
    }
}
