using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Readers;

namespace TransactionUploader.Infrastructure.Csv
{
    public class CsvFileReader: ICsvFileReader
    {
        public List<TModel> ReadRecords<TModel>(IFormFile formFile)
        {
            using var reader = new StreamReader(formFile.OpenReadStream());
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Configuration.MissingFieldFound = null;

            var records = csvReader.GetRecords<TModel>();
            return records.ToList();
        }
    }
}
