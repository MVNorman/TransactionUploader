using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Enums;

namespace TransactionUploader.Application.FormFile
{
    public static class FormFileExtensions
    {
        private static readonly IDictionary<string, FileFormat> SupportedFormats =
            new Dictionary<string, FileFormat>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"text/csv", FileFormat.Csv},
                {"text/xml", FileFormat.Xml}
            };

        public static FileFormat GetFileFormat(this IFormFile formFile)
        {
            var contentType = formFile.ContentType;

            if (contentType == null)
            {
                return FileFormat.Unknown;
            }

            return SupportedFormats.TryGetValue(contentType, out var mime) ? mime : FileFormat.Unknown;
        }
    }
}
