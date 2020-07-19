using Confluent.Kafka;

namespace BookShop.Infra.Messaging.Factories
{
    public static class ConsumerFactory
    {
        public static IConsumer<Ignore, string> Create(ConsumerConfig config)
        {
            var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            return consumer;
        }
    }
}