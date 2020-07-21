using System.Collections.Generic;
using TransactionUploader.Domain.Transaction.Enums;

namespace TransactionUploader.Domain.Transaction
{
    public static class TransactionDefaults
    {
        public static readonly IDictionary<TransactionStatus, string> TransactionStatusUnifiedFormats =
            new Dictionary<TransactionStatus, string>()
            {
                {TransactionStatus.Approved, "A"},
                {TransactionStatus.Failed, "R"},
                {TransactionStatus.Rejected, "R"},
                {TransactionStatus.Finished, "D"},
                {TransactionStatus.Done, "D"}
            };

    }
}
