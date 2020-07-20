using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Enums;
using TransactionUploader.Application.FormFile.Readers;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Application.Transaction.Models.Xml;
using TransactionUploader.Application.Transaction.TransactionHandlers.Contracts;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction.TransactionHandlers
{
    public class XmlTransactionHandler: IXmlTransactionHandler
    {
        private ITransactionHandler _successor;

        private readonly IXmlFileReader _xmlFileReader;
        private readonly ITransactionValidator _transactionValidator;
        private readonly IMapper _mapper;

        public XmlTransactionHandler(
            IXmlFileReader xmlFileReader, 
            ITransactionValidator transactionValidator,
            IMapper mapper)
        {
            _xmlFileReader = xmlFileReader;
            _transactionValidator = transactionValidator;
            _mapper = mapper;
        }

        public TransactionExportReadResult GetTransactionReadResult(IFormFile formFile, FileFormat fileFormat)
        {
            if (fileFormat != FileFormat.Xml) 
                return _successor?.GetTransactionReadResult(formFile, fileFormat);

            var result = new TransactionExportReadResult();

            var transactionXml = _xmlFileReader.ReadXml<XmlTransactionRoot>(formFile);
            var validationResult = _transactionValidator.Validate(transactionXml);

            if (validationResult.HasErrors)
            {
                result.ValidationResult = validationResult;
                var invalidRecords = transactionXml?.Transactions.Where(x => x.InValid);
                result.InvalidTransactionsJson = Newtonsoft.Json.JsonConvert.SerializeObject(invalidRecords);

                return result;
            }

            var transactions = _mapper.Map<TransactionEntity[]>(transactionXml.Transactions);
            result.TransactionsToExport.AddRange(transactions);

            return result;
        }

        public void SetSuccessor(ITransactionHandler successor)
        {
            _successor = successor;
        }
    }
}
