using System;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace BookShop.Domain.Books.Commands.Insert
{
    public class ExceptionHandler : RequestExceptionHandler<Handler, Exception>
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        protected override void Handle(Handler request, Exception exception, RequestExceptionHandlerState<Exception> state)
        {
            _logger.LogError(exception, exception.Message);
            state.SetHandled(exception);
        }
    }
}