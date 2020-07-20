using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using TransactionUploader.Application.FormFile.Contracts;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.TransactionLog.Contracts;
using TransactionUploader.Common;

namespace TransactionUploader.Application.Transaction.Commands.UploadTransactionCommand
{
    public class UploadTransactionHandler : IRequestHandler<UploadTransactionCommand, ValidationResult>
    {
        private readonly IFormFileValidator _fileValidator;
        private readonly ITransactionService _transactionService;
        private readonly ITransactionLogService _transactionLogService;

        public UploadTransactionHandler(
            IFormFileValidator fileValidator,
            ITransactionService transactionService,
            ITransactionLogService transactionLogService)
        {
            _fileValidator = fileValidator;
            _transactionService = transactionService;
            _transactionLogService = transactionLogService;
        }

        public async Task<ValidationResult> Handle(UploadTransactionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _fileValidator.Validate(request.FormFile);

            if (validationResult.HasErrors)
                return validationResult;

            try
            {
                var readExportResult = _transactionService.GetReadExportResult(request.FormFile);

                if (readExportResult.ValidationResult.HasErrors)
                {
                    await _transactionLogService.LogErrorAsync(readExportResult.InvalidTransactionsJson);
                    return readExportResult.ValidationResult;
                }

                var transactionIds = readExportResult.TransactionsToExport.Select(x => x.TransactionId);
                var exportedDuplicates = await _transactionService.GetByAsync(transactionIds);

                await _transactionService.InsertAsync(readExportResult.TransactionsToExport, exportedDuplicates);
                await _transactionService.UpdateAsync(readExportResult.TransactionsToExport, exportedDuplicates);
            }
            catch (Exception exception)
            {
                Log.Error(exception, "template");
                validationResult.Errors.Add("Unhandled exception has occured");
            }

            return validationResult;
        }
    }
}
