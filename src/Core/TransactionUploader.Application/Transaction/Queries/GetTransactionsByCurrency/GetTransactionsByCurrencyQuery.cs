using System.Collections.Generic;
using MediatR;
using TransactionUploader.Application.Transaction.Models;

namespace TransactionUploader.Application.Transaction.Queries.GetTransactionsByCurrency
{
    public class GetTransactionsByCurrencyQuery: IRequest<List<TransactionResponse>>
    {
        public GetTransactionsByCurrencyQuery(string currencyCode)
        {
            CurrencyCode = currencyCode;
        }

        public string CurrencyCode { get; }
    }
}
