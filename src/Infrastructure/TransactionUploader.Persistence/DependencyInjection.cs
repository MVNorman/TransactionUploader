using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionUploader.Application.Transaction;
using TransactionUploader.Persistence.Transaction;

namespace TransactionUploader.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TransactionUploaderDbContext>(x =>
                x.UseSqlServer(configuration.GetConnectionString("UploaderConnection")));


            //TODO: Implement generic addition repositories into DI pipeline
            services.AddTransient<ITransactionRepository, TransactionRepository>();


        }
    }
}
