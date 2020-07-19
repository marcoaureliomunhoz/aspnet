using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace BookShop.Infra.Messaging.Services
{
    public static class ConsumerConfigProvider
    {
        public static ConsumerConfig GetConfig(IConfiguration configuration)
        {
            return new ConsumerConfig
            {
                BootstrapServers = configuration.GetSection("MessagingSource:BootstrapServers").Value,
                GroupId = $"consumer-group-{configuration.GetSection("MessagingSource:GroupId").Value}",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                ReceiveMessageMaxBytes = (int)2000000000
            };
        }
    }
}