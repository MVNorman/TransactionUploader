using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace TransactionUploader.Application.MediatR
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;

        private const int AllowedRequestMilliseconds = 500;

        public RequestPerformanceBehaviour()
        {
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (AllowedRequestMilliseconds > _timer.ElapsedMilliseconds) 
                return response;

            var name = typeof(TRequest).Name;

            Log.Information($"Long Running Request: {name} ({_timer.ElapsedMilliseconds} milliseconds) {request}");

            return response;
        }
    }
}
