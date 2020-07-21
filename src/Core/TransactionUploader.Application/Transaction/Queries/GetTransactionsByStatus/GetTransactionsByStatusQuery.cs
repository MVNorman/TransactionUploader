using System.Collections.Generic;
using MediatR;
using TransactionUploader.Application.Transaction.Models;

namespace TransactionUploader.Application.Transaction.Queries.GetTransactionsByStatus
{
    public class GetTransactionsByStatusQuery : IRequest<List<TransactionResponse>>
    {
        public GetTransactionsByStatusQuery(string status)
        {
            Status = status;
        }

        public string Status { get; }
    }
}
