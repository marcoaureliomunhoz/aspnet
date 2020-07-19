using AutoFixture;
using BookShop.Domain.Books.Commands.Insert;
using FluentAssertions;
using Xunit;

namespace BookShop.Test.Unit.Domain.Books
{
    public class InsertCommandRequestTests
    {
        private Fixture _fixture;

        public InsertCommandRequestTests()
        {
            _fixture = new Fixture();
            _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 1000));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void RequestWithInvalidTitleShouldBeInvalid(string title)
        {
            var request = new Request
            {
                Title = title,
                Stock = _fixture.Create<int>()
            };

            request.Validate();

            request.Invalid.Should().BeTrue();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-44)]
        public void RequestWithInvalidStockShouldBeInvalid(int stock)
        {
            var request = new Request
            {
                Title = _fixture.Create<string>(),
                Stock = stock
            };

            request.Validate();

            request.Invalid.Should().BeTrue();
        }

        [Fact]
        public void RequesShouldBeValid()
        {
            var request = _fixture.Build<Request>().Create();

            request.Validate();

            request.Valid.Should().BeTrue();
        }
    }
}