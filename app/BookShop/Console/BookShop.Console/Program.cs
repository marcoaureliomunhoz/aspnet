using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookShop.Infra.Messaging.Extensions;
using BookShop.Infra.Messaging.Interfaces;
using BookShop.Infra.Messaging.Models;
using BookShop.Infra.Net.Extensions;
using BookShop.Infra.Net.Interfaces;
using BookShop.Infra.Net.Models;
using BookShop.Domain.Books.Commands.NewRequest;
using BookShop.Domain.Books;
using BookShop.Domain.Common;
using Newtonsoft.Json;
using System.Linq;

namespace BookShop.Console
{
    class Program
    {
        static IConfigurationRoot Configuration { get; set; }

        static async Task Main(string[] args)
        {
            System.Console.WriteLine("[START] BookShop.Console - Main");

            var options = new BookShopConsoleOptions();
            await RunApplication(options);

            System.Console.WriteLine("[FINISH] BookShop.Console - Main");
        }

        static async Task RunApplication(BookShopConsoleOptions options)
        {
            System.Console.WriteLine("[START] BookShop.Console - RunApplication");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddProducerInfraMessaging(Configuration)
                .AddInfraNet(Configuration)
                .BuildServiceProvider();
            
            var messageProducer = serviceProvider.GetService<IMessageProducer>();

            var messageConfiguration = new MessageProducerConfiguration
            {
                Topic = "BookShop.NewRequest"
            };

            var numbers = new int[options?.NumberMessages ?? 1];

            System.Console.WriteLine($"Number messages: {numbers.Length}");

            var books = await GetBooks(serviceProvider);
            var countBooks = books.Count();
            var randomBooks = new Random();
            var randomQuantity = new Random();

            var tasks = numbers.Select(async number => 
            {
                var bookIndex = randomBooks.Next(0, countBooks-1);
                var book = books[bookIndex];
                var value = new Request {
                    BookId = book.BookId,
                    Quantity = randomQuantity.Next(1, 1000)
                };

                try
                {
                    var deliveryResult = await messageProducer.ProduceAsync(messageConfiguration, value);
                    System.Console.WriteLine($"=> Offset: {deliveryResult?.Offset.Value}");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            });

            await Task.WhenAll(tasks);

            System.Console.WriteLine("[FINISH] BookShop.Console - RunApplication");
        }

        private static async Task<IList<Book>> GetBooks(ServiceProvider serviceProvider)
        {
            var webServices = serviceProvider.GetService<WebServicesConfiguration>();
            var webService = webServices.List.FirstOrDefault();
            var service = webService.Services.FirstOrDefault();
            var action = service.Actions.FirstOrDefault(x => x.Name == "ListAll");
            var urlBase = webService.Url + "/" + service.Url + "/" + action.Url;
            var httpService = serviceProvider.GetService<IHttpService>();
            var result = await httpService.GetAsync<Result<IList<Book>>>(urlBase);
            return result.Data;
        }
    }
}
