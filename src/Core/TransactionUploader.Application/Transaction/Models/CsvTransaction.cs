using CsvHelper.Configuration.Attributes;
using TransactionUploader.Domain.Transaction.Enums;

namespace TransactionUploader.Application.Transaction.Models
{
    public class CsvTransaction
    {
        [Index(0)]
        [Name("Transaction Identificator")]
        public string TransactionId { get; set; }

        [Index(1)]
        public decimal? Amount { get; set; }

        [Index(2)]
        [Name("Currency Code")]
        public string CurrencyCode { get; set; }

        [Index(3)]
        [Name("Transaction Date")]
        public string TransactionDate { get; set; }

        [Index(4)]
        public TransactionStatus? Status { get; set; }

        [Ignore]
        public bool InValid { get; set; }

        public void MarkAsInValid()
        {
            InValid = true;
        }
    }
}
