using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TransactionUploader.Application.FormFile.Readers
{
    public interface ICsvFileReader
    {
        List<TModel> ReadRecords<TModel>(IFormFile formFile);
    }
}
