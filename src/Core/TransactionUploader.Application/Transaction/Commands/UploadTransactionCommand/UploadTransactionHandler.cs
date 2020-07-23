using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using TransactionUploader.Application.Cache;
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
        private readonly ICacheManager _cacheManager;

        public UploadTransactionHandler(
            IFormFileValidator fileValidator,
            ITransactionService transactionService,
            ITransactionLogService transactionLogService,
            ICacheManager cacheManager)
        {
            _fileValidator = fileValidator;
            _transactionService = transactionService;
            _transactionLogService = transactionLogService;
            _cacheManager = cacheManager;
        }

        public async Task<ValidationResult> Handle(UploadTransactionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _fileValidator.Validate(request.FormFile);

            if (validationResult.HasErrors)
                return validationResult;

            try
            {
                var exportReadResult = _transactionService.GetReadExportResult(request.FormFile);

                if (exportReadResult.ValidationResult != null && exportReadResult.ValidationResult.HasErrors)
                {
                    await _transactionLogService.LogErrorAsync(exportReadResult.InvalidTransactionsJson);
                    return exportReadResult.ValidationResult;
                }

                var transactionIds = exportReadResult.Transactions.Select(x => x.TransactionId);
                var exportedDuplicates = await _transactionService.GetByAsync(transactionIds);

                await _transactionService.InsertAsync(exportReadResult.Transactions, exportedDuplicates);
                await _transactionService.UpdateAsync(exportReadResult.Transactions, exportedDuplicates);

                _cacheManager.RemoveByPrefix(TransactionCacheDefaults.MainTransactionPrefix);
            }
            catch (Exception exception)
            {
                Log.Error(exception, string.Empty);
                validationResult.Errors.Add("Upload wasn't successful");
            }

            return validationResult;
        }
    }
}
