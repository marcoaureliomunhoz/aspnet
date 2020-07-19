using BookShop.Infra.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BookShop.Domain.Common;
using MediatR;
using System;
using BookShop.Domain.Common.Pipelines;
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
                .AddBookShopDbContext(configuration)
                .AddBookShopRepositories()
                .AddBookShopMediatR()
                .AddBookShopEntityUnitOfWork()
                .AddJsonConfigurations();
            return services;
        }


        public static IServiceCollection AddBookShopDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var stringConnection = configuration.GetConnectionString("BookShopDbContext");
            services.AddDbContext<BookShopDbContext>(options => options.UseSqlServer(stringConnection));
            return services;
        }

        public static IServiceCollection AddBookShopRepositories(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblies(typeof(BookShopDbContext).Assembly)
                    .AddClasses(cls =>
                            cls.Where(type =>
                                type.Name.EndsWith("Repository") &&
                                !type.Name.StartsWith("I")))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());
            return services;
        }

        public static IServiceCollection AddBookShopMediatR(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MeasureTime<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateCommand<,>));

            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));
            // services.AddScoped(
            //     typeof(IRequestExceptionHandler<BookShop.Domain.Books.Commands.Insert.Handler, Exception>),
            //     typeof(BookShop.Domain.Books.Commands.Insert.ExceptionHandler));

            var assembly = AppDomain.CurrentDomain.Load("BookShop.Domain");

            services.AddMediatR(assembly);

            return services;
        }

        public static IServiceCollection AddBookShopEntityUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IEntityUnitOfWork, EntityUnitOfWork>();

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