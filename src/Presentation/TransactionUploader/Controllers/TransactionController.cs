using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionUploader.Application.Transaction.Commands.UploadTransactionCommand;

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

        [HttpGet]
        public string Get()
        {
            return "T";
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
