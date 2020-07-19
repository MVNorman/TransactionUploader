using Microsoft.Extensions.DependencyInjection;
using TransactionUploader.Application.FormFile.Readers;
using TransactionUploader.Infrastructure.Csv;
using TransactionUploader.Infrastructure.Xml;

namespace TransactionUploader.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICsvFileReader, CsvFileReader>();
            services.AddTransient<IXmlFileReader, XmlFileReader>();
        }
    }
}
