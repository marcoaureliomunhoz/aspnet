using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using BookShop.Infra.Messaging.Interfaces;

namespace BookShop.Worker
{
    public class MessageErrorHandler : IMessageErrorHandler
    {
        private readonly ILogger<MessageErrorHandler> _logger;

        public MessageErrorHandler(ILogger<MessageErrorHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(Exception ex)
        {
            System.Console.WriteLine(ex);
            _logger.LogError(ex, ex.Message);
            await Task.CompletedTask;
        }
    }
}