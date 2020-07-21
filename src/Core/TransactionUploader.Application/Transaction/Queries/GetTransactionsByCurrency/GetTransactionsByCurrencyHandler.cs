using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.Models;

namespace TransactionUploader.Application.Transaction.Queries.GetTransactionsByCurrency
{
    public class GetTransactionsByCurrencyHandler : IRequestHandler<GetTransactionsByCurrencyQuery, List<TransactionResponse>>
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionValidator _transactionValidator;

        public GetTransactionsByCurrencyHandler(
            ITransactionService transactionService,
            ITransactionValidator transactionValidator)
        {
            _transactionService = transactionService;
            _transactionValidator = transactionValidator;
        }

        public async Task<List<TransactionResponse>> Handle(GetTransactionsByCurrencyQuery request, CancellationToken cancellationToken)
        {
            var isValid = _transactionValidator.IsCurrencyCodeValid(request.CurrencyCode);
            if (!isValid)
                return new List<TransactionResponse>();

            var transactionResponses = await _transactionService.GetByCurrencyAsync(request.CurrencyCode);
            return transactionResponses;
        }
    }
}
