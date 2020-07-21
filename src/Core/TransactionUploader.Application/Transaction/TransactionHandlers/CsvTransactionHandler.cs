using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Enums;
using TransactionUploader.Application.FormFile.Readers;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.Models.FileReadModels;
using TransactionUploader.Application.Transaction.Models.FileReadModels.Csv;
using TransactionUploader.Application.Transaction.TransactionHandlers.Contracts;

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

        public TransactionReadResult GetReadResult(IFormFile formFile, FileFormat fileFormat)
        {
            if (fileFormat != FileFormat.Csv) 
                return _successor?.GetReadResult(formFile, fileFormat);

            var csvRecords = _csvFileReader.ReadRecords<CsvTransaction>(formFile);

            var transactionModels = _mapper.Map<List<TransactionModel>>(csvRecords);

            var result = _transactionValidator.GetValidatedReadResult(transactionModels);

            if (result.ValidationResult.HasErrors)
                return result;

            result.Transactions.AddRange(transactionModels);

            return result;
        }

        public void SetSuccessor(ITransactionHandler successor)
        {
            _successor = successor;
        }
    }
}
