using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TransactionUploader.Application.FormFile.Readers.Root
{
    public interface IFileReader
    {
        List<TModel> GetRecords<TModel>(IFormFile formFile);
    }
}
