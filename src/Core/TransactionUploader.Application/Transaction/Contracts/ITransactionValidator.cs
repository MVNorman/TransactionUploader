using System.Collections.Generic;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Application.Transaction.Models.Xml;
using TransactionUploader.Common;

namespace TransactionUploader.Application.Transaction.Contracts
{
    public interface ITransactionValidator
    {
        ValidationResult Validate(List<CsvTransaction> csvTransactions);
        ValidationResult Validate(XmlTransactionRoot xmlTransaction);
    }
}
