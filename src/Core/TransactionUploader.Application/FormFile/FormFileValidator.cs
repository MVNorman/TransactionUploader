using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Contracts;
using TransactionUploader.Application.FormFile.Enums;
using TransactionUploader.Common;

namespace TransactionUploader.Application.FormFile
{
    public class FormFileValidator: IFormFileValidator
    {
        //TODO: For hardcoded strings should be appropriate language resources
        public ValidationResult Validate(IFormFile file, int maxFileSize = 1000000)
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
            if (fileFormat == FileFormat.Unknown)
                validationResult.Errors.Add("Unknown format");

            if(file.Length > maxFileSize)
                validationResult.Errors.Add("Max file size 1 MB");

            return validationResult;
        }
    }
}
