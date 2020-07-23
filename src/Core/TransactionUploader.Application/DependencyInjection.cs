using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TransactionUploader.Application.FormFile;
using TransactionUploader.Application.FormFile.Contracts;
using TransactionUploader.Application.MediatR;
using TransactionUploader.Application.Transaction;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.FileReadHandlers;
using TransactionUploader.Application.Transaction.FileReadHandlers.Contracts;
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

            services.AddScoped<IFormFileValidator, FormFileValidator>();
            services.AddScoped<ITransactionValidator, TransactionValidator>();

            services.AddScoped<IXmlTransactionReadHandler, XmlTransactionReadHandler>();
            services.AddScoped<ICsvTransactionReadHandler, CsvTransactionReadHandler>();

            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionLogService, TransactionLogService>();
        }
    }
}
