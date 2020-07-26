using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using BookShop.Infra.Messaging.Models;
using Newtonsoft.Json;
using BookShop.Infra.Messaging.Exceptions;
using BookShop.Infra.Messaging.Interfaces;

namespace BookShop.Infra.Messaging.Services
{
    public class MessageProducer : IMessageProducer
    {
        private readonly ProducerConfig _producerConfig;

        public MessageProducer(ProducerConfig producerConfig)
        {
            _producerConfig = producerConfig;
        }

        public async Task<DeliveryResult<Null, string>> ProduceAsync<TValue>(
            MessageProducerConfiguration messageConfiguration,
            TValue value)
        {
            System.Console.WriteLine($"Producing to {messageConfiguration.Topic}");

            using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
            {
                try
                {
                    var deliveryResult = await producer.ProduceAsync(
                        messageConfiguration.Topic,
                        new Message<Null, string>()
                        {
                            Value = JsonConvert.SerializeObject(value)
                        });
                    return deliveryResult;
                }
                catch (Exception ex)
                {
                    throw new MessageProducerException(messageConfiguration.Topic, ex);
                }
            }
        }
    }
}