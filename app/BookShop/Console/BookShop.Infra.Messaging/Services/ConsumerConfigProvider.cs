using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace BookShop.Infra.Messaging.Services
{
    public static class ConsumerConfigProvider
    {
        public static ConsumerConfig GetConfig(IConfiguration configuration)
        {
            var autoCommit = GetAutoCommit(configuration);

            return new ConsumerConfig
            {
                BootstrapServers = configuration.GetSection("MessagingSource:BootstrapServers").Value,
                GroupId = $"consumer-group-{configuration.GetSection("MessagingSource:GroupId").Value}",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = autoCommit,
                ReceiveMessageMaxBytes = (int)2000000000
            };
        }

        private static bool GetAutoCommit(IConfiguration configuration) 
            => bool.TryParse(configuration.GetSection("MessagingSource:AutoCommit").Value, out var value) ? value : true;
    }
}