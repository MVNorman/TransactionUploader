using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.Models;

namespace TransactionUploader.Application.Transaction.Queries.GetTransactionsByDateRange
{
    public class GetTransactionsByDateRangeHandler: IRequestHandler<GetTransactionsByDateRangeQuery, List<TransactionResponse>>
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionValidator _transactionValidator;
        public GetTransactionsByDateRangeHandler(
            ITransactionService transactionService,
            ITransactionValidator transactionValidator)
        {
            _transactionService = transactionService;
            _transactionValidator = transactionValidator;
        }

        public async Task<List<TransactionResponse>> Handle(GetTransactionsByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var isValid = _transactionValidator.IsDateRangeValid(request.DateFrom, request.DateTo);
            if (!isValid)
                return new List<TransactionResponse>();

            return await _transactionService.GetByDateRangeAsync(request.DateFrom, request.DateTo);
        }
    }
}
