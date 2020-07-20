using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Readers.Root;

namespace TransactionUploader.Application.FormFile.Readers
{
    public interface IXmlFileReader: IFileReader
    {
        TModel ReadXml<TModel>(IFormFile formFile) where TModel: class;
    }
}
