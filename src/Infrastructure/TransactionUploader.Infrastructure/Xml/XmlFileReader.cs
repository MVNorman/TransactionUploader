using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Readers;

namespace TransactionUploader.Infrastructure.Xml
{
    public class XmlFileReader: IXmlFileReader
    {
        public TModel ReadXml<TModel>(IFormFile formFile) where TModel: class
        {
            var settings = new XmlReaderSettings
            {
                IgnoreWhitespace = true
            };

            using var reader = new StreamReader(formFile.OpenReadStream());
            using var xmlReader = XmlReader.Create(reader, settings);

            var serializer = new XmlSerializer(typeof(TModel));
            var models = serializer.Deserialize(xmlReader);

            if (models is TModel parsedRecord)
                return parsedRecord;

            return null;
        }
    }
}
