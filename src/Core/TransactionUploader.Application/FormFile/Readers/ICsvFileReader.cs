using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Readers.Root;

namespace TransactionUploader.Application.FormFile.Readers
{
    public interface ICsvFileReader: IFileReader
    {
        List<TModel> ReadRecords<TModel>(IFormFile formFile);
    }
}
