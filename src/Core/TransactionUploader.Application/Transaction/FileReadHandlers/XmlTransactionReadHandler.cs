using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TransactionUploader.Application.FormFile.Enums;
using TransactionUploader.Application.FormFile.Readers;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.FileReadHandlers.Contracts;
using TransactionUploader.Application.Transaction.Models.FileReadModels;
using TransactionUploader.Application.Transaction.Models.FileReadModels.Xml;

namespace TransactionUploader.Application.Transaction.FileReadHandlers
{
    public class XmlTransactionReadHandler: IXmlTransactionReadHandler
    {
        private ITransactionReadHandler _successor;

        private readonly IXmlFileReader _xmlFileReader;
        private readonly ITransactionValidator _transactionValidator;
        private readonly IMapper _mapper;

        public XmlTransactionReadHandler(
            IXmlFileReader xmlFileReader, 
            ITransactionValidator transactionValidator,
            IMapper mapper)
        {
            _xmlFileReader = xmlFileReader;
            _transactionValidator = transactionValidator;
            _mapper = mapper;
        }

        public TransactionReadResult GetReadResult(IFormFile formFile, SupportedFileFormat fileFormat)
        {
            if (fileFormat != SupportedFileFormat.Xml) 
                return _successor?.GetReadResult(formFile, fileFormat);

            var transactionXml = _xmlFileReader.ReadXml<XmlTransactionRoot>(formFile);

            var transactionModels = _mapper.Map<List<TransactionModel>>(transactionXml?.Transactions);

            var result = _transactionValidator.GetValidatedReadResult(transactionModels);

            if (result.ValidationResult.HasErrors)
                return result;

            result.Transactions.AddRange(transactionModels);

            return result;
        }

        public void SetSuccessor(ITransactionReadHandler successor)
        {
            _successor = successor;
        }
    }
}
