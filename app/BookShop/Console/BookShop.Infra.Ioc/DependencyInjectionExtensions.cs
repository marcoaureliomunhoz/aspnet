using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookShop.Domain.Common;
using MediatR;
using System;
using MediatR.Pipeline;
using Newtonsoft.Json;
using JsonNet.ContractResolvers;

namespace BookShop.Infra.Ioc
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApiInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddBookShopMediatR()
                .AddJsonConfigurations();
            return services;
        }


        public static IServiceCollection AddBookShopMediatR(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MeasureTime<,>));
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateCommand<,>));

            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));
            // services.AddScoped(
            //     typeof(IRequestExceptionHandler<BookShop.Domain.Books.Commands.Insert.Handler, Exception>),
            //     typeof(BookShop.Domain.Books.Commands.Insert.ExceptionHandler));

            var assembly = AppDomain.CurrentDomain.Load("BookShop.Domain");

            services.AddMediatR(assembly);

            return services;
        }

        public static IServiceCollection AddJsonConfigurations(this IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateSetterContractResolver()
            };
            return services;
        }
    }
}