using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BookShop.Domain.Books;
using BookShop.Domain.Books.Commands.Insert;
using BookShop.Domain.Books.Repositories;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace BookShop.Test.Unit.Domain.Books
{
    public class InsertCommandHandlerTests
    {
        private AutoMocker _mocker;
        private Fixture _fixture;
        private Handler _insertCommandHandler;
        private Request _request;

        public InsertCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _fixture = new Fixture();
            _insertCommandHandler = _mocker.CreateInstance<Handler>();
            _request = _fixture.Build<Request>().Create();
        }

        [Fact]
        public async Task ResultShouldBeFalse()
        {
            var response = await _insertCommandHandler.Handle(_request, CancellationToken.None);

            response.HasMessage.Should().BeFalse();
        }

        [Fact]
        public async Task HandlerShouldInsertRequest()
        {
            var repositoryMock = _mocker.GetMock<IBookWriteRepository>();
            
            await _insertCommandHandler.Handle(_request, CancellationToken.None);

            repositoryMock.Verify(m => 
                m.Insert(
                    It.Is<Book>(b => b.Title == _request.Title && b.Stock == _request.Stock),
                    CancellationToken.None
                ),
                Times.Once
            );
        }
    }
}