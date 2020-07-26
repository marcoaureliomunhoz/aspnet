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
using BookShop.Infra.Net.Extensions;

namespace BookShop.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public Worker(
            ILogger<Worker> logger,
            IConfiguration configuration,
            IServiceCollection services)
        {
            _logger = logger;
            _configuration = configuration;

            services
                .AddConsumerInfraMessaging(configuration)
                .AddProducerInfraMessaging(configuration)
                .AddInfraNet(configuration)
                .AddBookShopMediatR();

            services.AddTransient<IMessageHandler<Request>, NewRequestMessageHandler>();
            services.AddSingleton<IMessageErrorHandler, MessageErrorHandler>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("[START] Worker at {time}", DateTimeOffset.Now);

            Console.CancelKeyPress += new ConsoleCancelEventHandler(ConsoleCancel);

            var messageConsumer = _serviceProvider.GetService<IMessageConsumer>();

            await messageConsumer.Consume<Request>(
                new MessageConsumerConfiguration
                {
                    Topic = "BookShop.NewRequest"
                }
            );
            
            _logger.LogInformation("[FINISH] Worker at {time}", DateTimeOffset.Now);
            
            await Task.Delay(100, cancellationToken);
        }

        protected void ConsoleCancel(object sender, ConsoleCancelEventArgs args)
        {
            var messageConsumer = _serviceProvider.GetService<IMessageConsumer>();
            messageConsumer?.Deactive();
        }
    }
}
