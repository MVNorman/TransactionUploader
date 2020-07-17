using Microsoft.Extensions.DependencyInjection;
using TransactionUploader.Application.Xml;
using TransactionUploader.Infrastructure.Xml;

namespace TransactionUploader.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IXmlFileReader, XmlFileReader>();
        }
    }
}
