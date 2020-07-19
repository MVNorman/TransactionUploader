using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Readers;

namespace TransactionUploader.Infrastructure.Xml
{
    public class XmlFileReader: IXmlFileReader
    {
        public List<TModel> GetRecords<TModel>(IFormFile formFile)
        {
            throw new System.NotImplementedException();
        }
    }
}
