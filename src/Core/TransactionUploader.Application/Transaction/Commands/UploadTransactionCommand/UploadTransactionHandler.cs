using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Serilog;
using TransactionUploader.Application.FormFile;
using TransactionUploader.Application.FormFile.Contracts;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Application.Transaction.TransactionHandlers.Contracts;
using TransactionUploader.Common;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction.Commands.UploadTransactionCommand
{
    public class UploadTransactionHandler : IRequestHandler<UploadTransactionCommand, ValidationResult>
    {
        private readonly IFormFileValidator _formFileValidator;

        private readonly ICsvTransactionHandler _csvTransactionHandler;
        private readonly IXmlTransactionHandler _xmlTransactionHandler;

        private readonly ITransactionRepository _transactionRepository;

        public UploadTransactionHandler(
            IFormFileValidator formFileValidator,
            ICsvTransactionHandler csvTransactionHandler,
            IXmlTransactionHandler xmlTransactionHandler,
            ITransactionRepository transactionRepository)
        {
            _formFileValidator = formFileValidator;

            _csvTransactionHandler = csvTransactionHandler;
            _xmlTransactionHandler = xmlTransactionHandler;

            _transactionRepository = transactionRepository;
        }


        public async Task<ValidationResult> Handle(UploadTransactionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _formFileValidator.Validate(request.FormFile);

            if (validationResult.HasErrors)
                return validationResult;

            try
            {
                var exportResult = GetExportResult(request.FormFile);

                if (exportResult.ValidationResult.HasErrors)
                    return exportResult.ValidationResult;

                var exportTransactionIds = exportResult.Transactions.Select(x => x.TransactionId);

                var alreadyInserted = await _transactionRepository.GetByAsync(exportTransactionIds);
                var insertedTransactionIds = alreadyInserted
                    .Select(x => x.TransactionId)
                    .ToList();

                await HandleInsertAsync(exportResult.Transactions, insertedTransactionIds);

                var exportsToUpdate = exportResult.Transactions
                    .Where(x => insertedTransactionIds.Contains(x.TransactionId))
                    .ToDictionary(x=> x.TransactionId);

                alreadyInserted.ForEach(insertedTransaction =>
                {
                    insertedTransaction.Update(exportsToUpdate[insertedTransaction.TransactionId]);
                });

                if (alreadyInserted.Any()) 
                    _transactionRepository.UpdateRange(alreadyInserted);

                await _transactionRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Some template");
                validationResult.Errors.Add("Unhandled exception has occured");
            }

            return validationResult;
        }

        private async Task HandleInsertAsync(List<TransactionEntity> exportTransactions, List<string> insertedTransactionIds)
        {
            var transactionsToInsert = exportTransactions
                .Where(export => !insertedTransactionIds.Contains(export.TransactionId))
                .ToList();

            if (transactionsToInsert.Any())
                await _transactionRepository.InsertRangeAsync(transactionsToInsert);
        }

        private TransactionExportResult GetExportResult(IFormFile formFile)
        {
            var fileFormat = formFile.GetFileFormat();

            _csvTransactionHandler.SetSuccessor(_xmlTransactionHandler);

            return _csvTransactionHandler.HandleRequest(formFile, fileFormat);
        }
    }
}
