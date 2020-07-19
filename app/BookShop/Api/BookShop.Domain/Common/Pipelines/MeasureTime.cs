using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookShop.Domain.Common.Pipelines
{
    public class MeasureTime<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<MeasureTime<TRequest, TResponse>> _logger;

        public MeasureTime(ILogger<MeasureTime<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
       
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var stopWatch = Stopwatch.StartNew();
            var result = await next();
            var elapsed = stopWatch.Elapsed;
            var name = typeof(TRequest).FullName;
            var message = $"#log: Request {name} executed at {elapsed}ms";
            _logger.LogInformation(message);
            return result;
        }
    }
}