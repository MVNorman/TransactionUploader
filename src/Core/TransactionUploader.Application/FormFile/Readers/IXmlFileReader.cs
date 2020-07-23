using Microsoft.AspNetCore.Http;

namespace TransactionUploader.Application.FormFile.Readers
{
    public interface IXmlFileReader
    {
        TModel ReadXml<TModel>(IFormFile formFile) where TModel: class;
    }
}
