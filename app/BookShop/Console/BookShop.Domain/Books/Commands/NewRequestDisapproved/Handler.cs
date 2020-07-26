using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BookShop.Infra.Messaging.Interfaces;
using BookShop.Infra.Messaging.Models;

namespace BookShop.Domain.Books.Commands.NewRequestDisapproved
{
    public class Handler : INotificationHandler<NewRequestDisapproved.Notification>
    {
        private readonly IMessageProducer _messageProducer;

        public Handler(IMessageProducer messageProducer)
        {
            _messageProducer = messageProducer;
        }

        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            try
            {
                var messageConfiguration = new MessageProducerConfiguration
                {
                    Topic = "BookShop.NewRequest.Disapproved"
                };

                var deliveryResult = await _messageProducer.ProduceAsync(messageConfiguration, notification);
                System.Console.WriteLine($"=> Offset: {deliveryResult?.Offset.Value}");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}