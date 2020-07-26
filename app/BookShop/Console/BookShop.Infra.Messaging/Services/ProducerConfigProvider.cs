using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace BookShop.Infra.Messaging.Services
{
    public static class ProducerConfigProvider
    {
        public static ProducerConfig GetConfig(IConfiguration configuration)
        {
            return new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("MessagingSource:BootstrapServers").Value
            };
        }
    }
}