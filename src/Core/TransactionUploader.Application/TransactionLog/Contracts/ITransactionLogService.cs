using System.Threading.Tasks;

namespace TransactionUploader.Application.TransactionLog.Contracts
{
    public interface ITransactionLogService
    {
        Task LogErrorAsync(string invalidTransactionsJson);
    }
}
