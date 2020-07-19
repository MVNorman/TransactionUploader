using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Enums;
using TransactionUploader.Application.FormFile.Readers;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Application.Transaction.TransactionHandlers.Contracts;
using TransactionUploader.Application.TransactionLog;
using TransactionUploader.Domain.Transaction;
using TransactionUploader.Domain.Transaction.Enums;
using TransactionUploader.Domain.TransactionLog;

namespace TransactionUploader.Application.Transaction.TransactionHandlers
{
    public class CsvTransactionHandler : ICsvTransactionHandler
    {
        private ITransactionHandler _successor;

        private readonly IMapper _mapper;
        private readonly ICsvFileReader _csvFileReader;
        private readonly ITransactionValidator _transactionValidator;
        private readonly ITransactionLogRepository _transactionLogRepository;

        public CsvTransactionHandler(
            IMapper mapper,
            ICsvFileReader csvFileReader,
            ITransactionValidator transactionValidator,
            ITransactionLogRepository transactionLogRepository)
        {
            _mapper = mapper;
            _csvFileReader = csvFileReader;
            _transactionValidator = transactionValidator;
            _transactionLogRepository = transactionLogRepository;
        }

        public TransactionExportResult HandleRequest(IFormFile formFile, FileFormat fileFormat)
        {
            if (fileFormat != FileFormat.Csv) 
                return _successor?.HandleRequest(formFile, fileFormat);

            var result = new TransactionExportResult();

            var csvRecords = _csvFileReader.GetRecords<CsvTransaction>(formFile);

             var validationResult = _transactionValidator.Validate(csvRecords);
             if (validationResult.HasErrors)
             {
                 result.ValidationResult = validationResult;
                 SaveTransactionLog(csvRecords);

                 return result;
             }

            var transactions = _mapper.Map<List<TransactionEntity>>(csvRecords);
            result.Transactions.AddRange(transactions);

            return result;
        }

        public void SetSuccessor(ITransactionHandler successor)
        {
            _successor = successor;
        }

        private void SaveTransactionLog(IEnumerable<CsvTransaction> records)
        {
            var invalidRecords = records.Where(x => x.InValid);

            var logEntity = new TransactionLogEntity()
            {
                CreatedAt = DateTime.UtcNow,
                InvalidTransactionsJson = Newtonsoft.Json.JsonConvert.SerializeObject(invalidRecords),
                TransactionType = TransactionType.Csv
            };

            _transactionLogRepository.Insert(logEntity);
            _transactionLogRepository.SaveChanges();
        }
    }
}
