using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionUploader.Common.Extensions;

namespace TransactionUploader.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TransactionUploaderDbContext>(x =>
                x.UseSqlServer(configuration.GetConnectionString("UploaderConnection")));

            AddRepositories(services);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            var repositoryTypes = AppDomain.CurrentDomain
                .GetImplementationsTypes("TransactionUploader", "Repository");

            foreach (var implementationType in repositoryTypes)
            {
                foreach (var interfaceType in implementationType.GetInterfaces())
                    services.AddTransient(interfaceType, implementationType);
            }
        }
    }
}
