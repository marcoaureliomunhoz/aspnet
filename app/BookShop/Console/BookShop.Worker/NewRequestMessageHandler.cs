using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediatR;
using BookShop.Infra.Messaging.Interfaces;
using BookShop.Domain.Books.Commands.NewRequest;

namespace BookShop.Worker
{
    public class NewRequestMessageHandler : IMessageHandler<Request>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<NewRequestMessageHandler> _logger;

        public NewRequestMessageHandler(
            IMediator mediator,
            ILogger<NewRequestMessageHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<bool> HandleMessage(Request message)
        {
            try
            {
                await _mediator.Publish(new Domain.Books.Commands.NewRequest.Notification
                {
                    BookId = message.BookId,
                    Quantity = message.Quantity
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task HandleException(Exception ex)
        {
            System.Console.WriteLine($"{ex.Message}");
            _logger.LogError(ex, ex.Message);
            await Task.CompletedTask;
        }
    }
}