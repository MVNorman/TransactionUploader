using System;
using TransactionUploader.Domain.Transaction.Enums;

namespace TransactionUploader.Application.Transaction.Models.FileReadModels
{
    public class TransactionModel
    {
        public string TransactionId { get; set; }

        public string CurrencyCode { get; set; }

        public DateTime? TransactionDate { get; set; }

        public decimal? Amount { get; set; }

        public TransactionStatus? Status { get; set; }
        public TransactionType Type { get; set; }

        public bool InValid { get; set; }

        public void MarkAsInValid()
        {
            InValid = true;
        }
    }
}
