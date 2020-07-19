using BookShop.Infra.Messaging.Factories;
using BookShop.Infra.Messaging.Interfaces;
using BookShop.Infra.Messaging.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Infra.Messaging.Extensions
{
    public static class InfraMessagingSetupExtensions
    {
        public static IServiceCollection AddInfraMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var consumerConfig = ConsumerConfigProvider.GetConfig(configuration);
            services.AddSingleton<IMessageConsumer>(new MessageConsumer(ConsumerFactory.Create(consumerConfig)));
            return services;
        }
    }
}