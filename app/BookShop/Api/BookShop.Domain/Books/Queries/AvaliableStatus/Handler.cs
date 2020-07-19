using System.Threading;
using System.Threading.Tasks;
using BookShop.Domain.Books.Repositories;
using BookShop.Domain.Common;
using MediatR;

namespace BookShop.Domain.Books.Queries.AvaliableStatus
{
    public class Handler : IRequestHandler<Request, Result<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IBookReadRepository _bookReadRepository;

        public Handler(
            IMediator mediator,
            IBookReadRepository bookReadRepository)
        {
            _mediator = mediator;
            _bookReadRepository = bookReadRepository;
        }
        
        public async Task<Result<bool>> Handle(Request request, CancellationToken cancellationToken)
        {
            var book = await _bookReadRepository.GetById(request.BookId);
            
            if (book.Stock.isLessThanOrEqualToZero())
                return ResultFactory.Nok();

            return ResultFactory.Ok();
        }
    }
}