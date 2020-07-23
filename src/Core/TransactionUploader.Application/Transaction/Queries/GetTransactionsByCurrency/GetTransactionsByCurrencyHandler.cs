using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TransactionUploader.Application.Cache;
using TransactionUploader.Application.Transaction.Contracts;
using TransactionUploader.Application.Transaction.Models;

namespace TransactionUploader.Application.Transaction.Queries.GetTransactionsByCurrency
{
    public class GetTransactionsByCurrencyHandler : IRequestHandler<GetTransactionsByCurrencyQuery, List<TransactionResponse>>
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionValidator _transactionValidator;
        private readonly ICacheManager _cacheManager;

        public GetTransactionsByCurrencyHandler(
            ITransactionService transactionService,
            ITransactionValidator transactionValidator,
            ICacheManager cacheManager)
        {
            _transactionService = transactionService;
            _transactionValidator = transactionValidator;
            _cacheManager = cacheManager;
        }

        public async Task<List<TransactionResponse>> Handle(GetTransactionsByCurrencyQuery request, CancellationToken cancellationToken)
        {
            var isValid = _transactionValidator.IsCurrencyCodeValid(request.CurrencyCode);
            if (!isValid)
                return new List<TransactionResponse>();

            var cacheKey = string.Format(TransactionCacheDefaults.TransactionsByCurrencyCacheFormat, request.CurrencyCode);

            return await _cacheManager.GetOrCreateAsync(cacheKey, 
                () => _transactionService.GetByCurrencyAsync(request.CurrencyCode));
        }
    }
}
