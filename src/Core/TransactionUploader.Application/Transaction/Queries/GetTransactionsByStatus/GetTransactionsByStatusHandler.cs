using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.Models;

namespace TransactionUploader.Application.Transaction.Queries.GetTransactionsByStatus
{
    public class GetTransactionsByStatusHandler: IRequestHandler<GetTransactionsByStatusQuery, List<TransactionResponse>>
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionValidator _transactionValidator;

        public GetTransactionsByStatusHandler(
            ITransactionService transactionService,
            ITransactionValidator transactionValidator)
        {
            _transactionService = transactionService;
            _transactionValidator = transactionValidator;
        }

        public async Task<List<TransactionResponse>> Handle(GetTransactionsByStatusQuery request, CancellationToken cancellationToken)
        {
            var isValid = _transactionValidator.IsStatusValid(request.Status);
            if(!isValid)
                return new List<TransactionResponse>();

            var transactions = await _transactionService.GetByStatusAsync(request.Status);
            return transactions;
        }
    }
}
