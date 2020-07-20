using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TransactionUploader.Application.FormFile;
using TransactionUploader.Application.FormFile.Contracts;
using TransactionUploader.Application.MediatR;
using TransactionUploader.Application.Transaction;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.TransactionHandlers;
using TransactionUploader.Application.Transaction.TransactionHandlers.Contracts;
using TransactionUploader.Application.TransactionLog;
using TransactionUploader.Application.TransactionLog.Contracts;

namespace TransactionUploader.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection));

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));

            services.AddTransient<IFormFileValidator, FormFileValidator>();
            services.AddTransient<ITransactionValidator, TransactionValidator>();

            services.AddTransient<IXmlTransactionHandler, XmlTransactionHandler>();
            services.AddTransient<ICsvTransactionHandler, CsvTransactionHandler>();

            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ITransactionLogService, TransactionLogService>();
        }
    }
}
