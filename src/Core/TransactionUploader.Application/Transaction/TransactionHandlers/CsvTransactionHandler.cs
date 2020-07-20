using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Enums;
using TransactionUploader.Application.FormFile.Readers;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Application.Transaction.TransactionHandlers.Contracts;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction.TransactionHandlers
{
    public class CsvTransactionHandler : ICsvTransactionHandler
    {
        private ITransactionHandler _successor;

        private readonly IMapper _mapper;
        private readonly ICsvFileReader _csvFileReader;
        private readonly ITransactionValidator _transactionValidator;

        public CsvTransactionHandler(
            IMapper mapper,
            ICsvFileReader csvFileReader,
            ITransactionValidator transactionValidator)
        {
            _mapper = mapper;
            _csvFileReader = csvFileReader;
            _transactionValidator = transactionValidator;
        }

        public TransactionExportReadResult GetTransactionReadResult(IFormFile formFile, FileFormat fileFormat)
        {
            if (fileFormat != FileFormat.Csv) 
                return _successor?.GetTransactionReadResult(formFile, fileFormat);

            var result = new TransactionExportReadResult();

            var csvRecords = _csvFileReader.ReadRecords<CsvTransaction>(formFile);

             var validationResult = _transactionValidator.Validate(csvRecords);
             if (validationResult.HasErrors)
             {
                 result.ValidationResult = validationResult;

                 var invalidRecords = csvRecords.Where(x => x.InValid);
                 result.InvalidTransactionsJson = Newtonsoft.Json.JsonConvert.SerializeObject(invalidRecords);

                 return result;
             }

            var transactions = _mapper.Map<List<TransactionEntity>>(csvRecords);
            result.TransactionsToExport.AddRange(transactions);

            return result;
        }

        public void SetSuccessor(ITransactionHandler successor)
        {
            _successor = successor;
        }
    }
}
