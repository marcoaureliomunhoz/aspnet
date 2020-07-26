using BookShop.Infra.Messaging.Factories;
using BookShop.Infra.Messaging.Interfaces;
using BookShop.Infra.Messaging.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Infra.Messaging.Extensions
{
    public static class InfraMessagingSetupExtensions
    {
        public static IServiceCollection AddConsumerInfraMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var consumerConfig = ConsumerConfigProvider.GetConfig(configuration);
            services.AddSingleton<IMessageConsumer>(
                new MessageConsumer(consumerConfig, ConsumerFactory.Create(consumerConfig), services));

            return services;
        }

        public static IServiceCollection AddProducerInfraMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var producerConfig = ProducerConfigProvider.GetConfig(configuration);
            services.AddSingleton<IMessageProducer>(new MessageProducer(producerConfig));

            return services;
        }
    }
}