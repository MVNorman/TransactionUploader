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
                Approved => "A",
                Failed => "R",
                Rejected => "R",
                Finished => "D",
                Done => "D",
                _ => "Unknown"
            };
        }
    }
}
