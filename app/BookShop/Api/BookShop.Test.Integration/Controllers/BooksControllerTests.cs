using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BookShop.Domain.Books;
using BookShop.Domain.Common;
using BookShop.Test.Integration.Common;
using FluentAssertions;
using JsonNet.ContractResolvers;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace BookShop.Test.Integration.Controllers
{
    public class BooksControllerTests : IClassFixture<CustomWebApplicationFactory<BookShop.Api.Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<BookShop.Api.Startup> _factory;

        public BooksControllerTests(CustomWebApplicationFactory<BookShop.Api.Startup> factory)
        {
            _factory = factory;
            _client = factory
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
        }

        [Fact]
        public async Task ListAllShouldReturnOK()
        {
            var response = await _client.GetAsync("/Books/ListAll");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ListAllShouldReturnList()
        {
            var response = await _client.GetAsync("/Books/ListAll");
            var booksInMemory = new Result<List<Book>>(BookShopInMemoryDbForGeneralTesting.GetBooks());

            var content = await response.Content.ReadAsStringAsync();

            var booksInResult = JsonConvert.DeserializeObject<Result<List<Book>>>(content);
            // , new JsonSerializerSettings()
            // {
            //     ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            //     ContractResolver = new PrivateSetterContractResolver()
            // });
            var resultHasAllExpectedBooks = booksInResult.Data.All(bookr => booksInMemory.Data.Any(bookm => bookm.BookId == bookr.BookId));
            resultHasAllExpectedBooks.Should().BeTrue();
        }
    }
}