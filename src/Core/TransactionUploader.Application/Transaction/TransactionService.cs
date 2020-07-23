using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TransactionUploader.Application.FormFile;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.FileReadHandlers.Contracts;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Application.Transaction.Models.FileReadModels;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ICsvTransactionReadHandler _csvTransactionReadHandler;
        private readonly IXmlTransactionReadHandler _xmlTransactionReadHandler;

        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(
            ICsvTransactionReadHandler csvTransactionReadHandler,
            IXmlTransactionReadHandler xmlTransactionReadHandler,
            ITransactionRepository transactionRepository,
            IMapper mapper)
        {
            _csvTransactionReadHandler = csvTransactionReadHandler;
            _xmlTransactionReadHandler = xmlTransactionReadHandler;

            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public TransactionReadResult GetReadExportResult(IFormFile formFile)
        {
            var fileFormat = formFile.GetFileFormat();

            _csvTransactionReadHandler.SetSuccessor(_xmlTransactionReadHandler);

            return _csvTransactionReadHandler.GetReadResult(formFile, fileFormat);
        }

        public async Task InsertAsync(List<TransactionModel> transactionsToExport, List<TransactionEntity> exportedDuplicates)
        {
            var duplicateIds = exportedDuplicates
                .Select(x => x.TransactionId)
                .ToList();

            var transactionsToInsert = transactionsToExport
                .Where(export => !duplicateIds.Contains(export.TransactionId))
                .ToList();

            if (!transactionsToInsert.Any())
                return;

            var mappedExports = _mapper.Map<IEnumerable<TransactionEntity>>(transactionsToExport);
            await _transactionRepository.AddRangeAsync(mappedExports);
        }

        public async Task UpdateAsync(List<TransactionModel> transactionsToExport, List<TransactionEntity> exportedDuplicates)
        {
            if (!exportedDuplicates.Any())
                return;

            var mappedTransaction = _mapper.Map<List<TransactionEntity>>(transactionsToExport);

            var duplicateIds = exportedDuplicates
            .Select(x => x.TransactionId)
            .ToList();

            var transactionsToUpdate = mappedTransaction
                .Where(x => duplicateIds.Contains(x.TransactionId))
                .ToDictionary(x => x.TransactionId);

            exportedDuplicates.ForEach(duplicate =>
            {
                duplicate.Update(transactionsToUpdate[duplicate.TransactionId]);
            });

            await _transactionRepository.UpdateRangeAsync(exportedDuplicates);
        }

        public async Task<List<TransactionEntity>> GetByAsync(IEnumerable<string> transactionIds)
        {
            return await _transactionRepository.GetByAsync(transactionIds);
        }

        public async Task<List<TransactionResponse>> GetByCurrencyAsync(string currencyCode)
        {
            return await _transactionRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(x=> x.CurrencyCode.Equals(currencyCode))
                .ProjectTo<TransactionResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<TransactionResponse>> GetByStatusAsync(string statusInUnifiedFormat)
        {
            var transactionStatuses = 
                TransactionDefaults.TransactionStatusUnifiedFormats
                .Where(x =>
                    x.Value.Equals(statusInUnifiedFormat, StringComparison.OrdinalIgnoreCase))
                .Select(x=> x.Key)
                .ToList();

            var transactions = await _transactionRepository
                .GetQueryable()
                .Where(x => transactionStatuses.Contains(x.Status))
                .ProjectTo<TransactionResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return transactions;
        }

        public async Task<List<TransactionResponse>> GetByDateRangeAsync(DateTime dateFrom, DateTime dateTo)
        {
            var transactions = await _transactionRepository
                .GetQueryable()
                .Where(x => x.TransactionDate >= dateFrom && x.TransactionDate <= dateTo)
                .ProjectTo<TransactionResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return transactions;
        }
    }
}
