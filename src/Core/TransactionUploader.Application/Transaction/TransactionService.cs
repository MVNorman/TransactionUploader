using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TransactionUploader.Application.FormFile;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Application.Transaction.TransactionHandlers.Contracts;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ICsvTransactionHandler _csvTransactionHandler;
        private readonly IXmlTransactionHandler _xmlTransactionHandler;

        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(
            ICsvTransactionHandler csvTransactionHandler,
            IXmlTransactionHandler xmlTransactionHandler,
            ITransactionRepository transactionRepository,
            IMapper mapper)
        {
            _csvTransactionHandler = csvTransactionHandler;
            _xmlTransactionHandler = xmlTransactionHandler;

            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public TransactionExportReadResult GetReadExportResult(IFormFile formFile)
        {
            var fileFormat = formFile.GetFileFormat();

            _csvTransactionHandler.SetSuccessor(_xmlTransactionHandler);

            return _csvTransactionHandler.GetTransactionReadResult(formFile, fileFormat);
        }

        public async Task InsertAsync(List<TransactionEntity> transactionsToExport, List<TransactionEntity> exportedDuplicates)
        {
            var duplicateIds = exportedDuplicates
                .Select(x => x.TransactionId)
                .ToList();

            var transactionsToInsert = transactionsToExport
                .Where(export => !duplicateIds.Contains(export.TransactionId))
                .ToList();

            if (!transactionsToInsert.Any())
                return;

            await _transactionRepository.InsertRangeAsync(transactionsToInsert);
            await _transactionRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<TransactionEntity> transactionsToExport, List<TransactionEntity> exportedDuplicates)
        {
            if (!exportedDuplicates.Any())
                return;

            var duplicateIds = exportedDuplicates
            .Select(x => x.TransactionId)
            .ToList();

            var transactionsToUpdate = transactionsToExport
                .Where(x => duplicateIds.Contains(x.TransactionId))
                .ToDictionary(x => x.TransactionId);

            exportedDuplicates.ForEach(duplicate =>
            {
                duplicate.Update(transactionsToUpdate[duplicate.TransactionId]);
            });

            _transactionRepository.UpdateRange(exportedDuplicates);

            await _transactionRepository.SaveChangesAsync();
        }

        public async Task<List<TransactionEntity>> GetByAsync(IEnumerable<string> transactionIds)
        {
            return await _transactionRepository.GetByAsync(transactionIds);
        }

        public async Task<List<TransactionResponse>> GetByAsync(string currencyCode)
        {
            return await _transactionRepository
                .Queryable()
                .AsNoTracking()
                .ProjectTo<TransactionResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
