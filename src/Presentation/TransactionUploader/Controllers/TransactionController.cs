using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionUploader.Application.Transaction.Commands.UploadTransactionCommand;
using TransactionUploader.Application.Transaction.Queries.GetTransactionsByCurrency;
using TransactionUploader.Application.Transaction.Queries.GetTransactionsByDateRange;
using TransactionUploader.Application.Transaction.Queries.GetTransactionsByStatus;

namespace TransactionUploader.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController: ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("currency")]
        public async Task<ActionResult> GetByCurrencyCodeAsync(string currency)
        {
            var transactions = await _mediator.Send(new GetTransactionsByCurrencyQuery(currency));
            if (transactions.Count == default)
                return NoContent();

            return Ok(transactions);
        }

        [HttpGet("status")]
        public async Task<ActionResult> GetByStatusAsync(string status)
        {
            var transactions = await _mediator.Send(new GetTransactionsByStatusQuery(status));
            if (transactions.Count == default)
                return NoContent();

            return Ok(transactions);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult> GetByDateRangeAsync(DateTime dateFrom, DateTime dateTo)
        {
            var transactions = await _mediator.Send(new GetTransactionsByDateRangeQuery(dateFrom, dateTo));
            if (transactions.Count == default)
                return NoContent();

            return Ok(transactions);
        }

        [HttpPost]
        public async Task<ActionResult> UploadAsync(IFormFile formFile)
        {
            var result = await _mediator.Send(new UploadTransactionCommand(formFile));
            if (result.HasErrors)
                return BadRequest(result.Errors);

            return Ok();
        }
    }
}
