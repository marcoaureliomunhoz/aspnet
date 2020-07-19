using BookShop.Infra.Net.Interfaces;
using BookShop.Infra.Net.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Infra.Net.Extensions
{
    public static class InfraNetSetupExtensions
    {
        public static IServiceCollection AddInfraNet(this IServiceCollection services, IConfiguration configuration)
        {
            var webServicesConfiguration = WebServicesConfigurationProvider.GetConfig(configuration);
            services.AddSingleton(webServicesConfiguration);

            services.AddSingleton<IHttpService>(new HttpService());
            return services;
        }
    }
}