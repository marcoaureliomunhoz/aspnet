using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookShop.Infra.Messaging.Extensions;
using BookShop.Infra.Messaging.Interfaces;
using BookShop.Infra.Messaging.Models;
using BookShop.Infra.Ioc;
using BookShop.Domain.Books.Commands.NewRequest;
using MediatR;
using BookShop.Infra.Net.Extensions;

namespace BookShop.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediator _mediator;

        public Worker(
            ILogger<Worker> logger,
            IConfiguration configuration,
            IServiceCollection services,
            IMediator mediator)
        {
            _logger = logger;
            _configuration = configuration;
            _mediator = mediator;

            services
                .AddInfraMessaging(configuration)
                .AddInfraNet(configuration);

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("[START] Worker at {time}", DateTimeOffset.Now);

            Console.CancelKeyPress += new ConsoleCancelEventHandler(ConsoleCancel);

            var messageConsumer = _serviceProvider.GetService<IMessageConsumer>();

            messageConsumer.Consume<Request>(
                new MessageConsumerConfiguration
                {
                    Topic = "BookShop.NewRequest"
                },
                OnMessageValue,
                OnMessageText,
                OnError
            );
            
            _logger.LogInformation("[FINISH] Worker at {time}", DateTimeOffset.Now);
            
            await Task.Delay(100, cancellationToken);
        }

        protected void ConsoleCancel(object sender, ConsoleCancelEventArgs args)
        {
            var messageConsumer = _serviceProvider.GetService<IMessageConsumer>();
            messageConsumer?.Deactive();
        }

        private void OnMessageValue(Request request)
        {
            _mediator.Publish(new Domain.Books.Commands.NewRequest.Notification
            {
                BookId = request.BookId,
                Quantity = request.Quantity
            });
        }

        private void OnMessageText(string text, Exception ex)
        {
            _logger.LogInformation(text, ex.Message);
        }

        private void OnError(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
        }
    }
}
