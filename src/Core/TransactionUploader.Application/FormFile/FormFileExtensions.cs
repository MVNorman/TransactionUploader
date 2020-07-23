using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Enums;

namespace TransactionUploader.Application.FormFile
{
    public static class FormFileExtensions
    {
        private static readonly IDictionary<string, SupportedFileFormat> SupportedFormats =
            new Dictionary<string, SupportedFileFormat>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"text/csv", SupportedFileFormat.Csv},
                {"application/vnd.ms-excel", SupportedFileFormat.Csv },
                {"text/xml", SupportedFileFormat.Xml},
                {"application/xml", SupportedFileFormat.Xml}
            };

        public static SupportedFileFormat GetFileFormat(this IFormFile formFile)
        {
            var contentType = formFile.ContentType;

            if (contentType == null)
            {
                return SupportedFileFormat.Unknown;
            }

            return SupportedFormats.TryGetValue(contentType, out var mime) ? mime : SupportedFileFormat.Unknown;
        }
    }
}
