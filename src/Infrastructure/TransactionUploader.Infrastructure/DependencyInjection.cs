using Microsoft.Extensions.DependencyInjection;
using TransactionUploader.Application.Cache;
using TransactionUploader.Application.FormFile.Readers;
using TransactionUploader.Infrastructure.Cache;
using TransactionUploader.Infrastructure.Cache.Redis;
using TransactionUploader.Infrastructure.Csv;
using TransactionUploader.Infrastructure.Xml;

namespace TransactionUploader.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, bool isDevelopment)
        {
            services.AddScoped<ICsvFileReader, CsvFileReader>();
            services.AddScoped<IXmlFileReader, XmlFileReader>();

            if (isDevelopment)
            {
                services.AddMemoryCache();
                services.AddSingleton<ICacheManager, MemoryCacheManager>();
            }
            else
                services.AddSingleton<ICacheManager, RedisCacheManager>();
        }
    }
}
