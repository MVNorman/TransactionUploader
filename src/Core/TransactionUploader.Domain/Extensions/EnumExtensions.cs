using TransactionUploader.Domain.Transaction;
using TransactionUploader.Domain.Transaction.Enums;
using static TransactionUploader.Domain.Transaction.Enums.TransactionStatus;

namespace TransactionUploader.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string ToUnifiedFormat(this TransactionStatus status)
        {
            return status switch
            {
                Approved => TransactionDefaults.TransactionStatusUnifiedFormats[Approved],
                Failed => TransactionDefaults.TransactionStatusUnifiedFormats[Failed],
                Rejected => TransactionDefaults.TransactionStatusUnifiedFormats[Rejected],
                Finished => TransactionDefaults.TransactionStatusUnifiedFormats[Finished],
                Done => TransactionDefaults.TransactionStatusUnifiedFormats[Done],
                _ => "Unknown"
            };
        }
    }
}
