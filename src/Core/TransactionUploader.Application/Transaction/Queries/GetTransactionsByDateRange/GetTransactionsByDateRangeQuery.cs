using System;
using System.Collections.Generic;
using MediatR;
using TransactionUploader.Application.Transaction.Models;

namespace TransactionUploader.Application.Transaction.Queries.GetTransactionsByDateRange
{
    public class GetTransactionsByDateRangeQuery: IRequest<List<TransactionResponse>>
    {
        public GetTransactionsByDateRangeQuery(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }
    }
}
