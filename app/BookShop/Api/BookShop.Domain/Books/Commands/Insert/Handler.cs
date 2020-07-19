using System.Threading;
using System.Threading.Tasks;
using BookShop.Domain.Books.Repositories;
using BookShop.Domain.Common;
using MediatR;

namespace BookShop.Domain.Books.Commands.Insert
{
    public class Handler : IRequestHandler<Request, Result<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IBookWriteRepository _bookWriteRepository;
        private readonly IEntityUnitOfWork _entityUnitOfWork;

        public Handler(
            IMediator mediator,
            IBookWriteRepository bookWriteRepository,
            IEntityUnitOfWork entityUnitOfWork)
        {
            _mediator = mediator;
            _bookWriteRepository = bookWriteRepository;
            _entityUnitOfWork = entityUnitOfWork;
        }
        
        public async Task<Result<bool>> Handle(Request request, CancellationToken cancellationToken)
        {
            var newBook = new Book(request.Title, request.Stock);
            await _bookWriteRepository.Insert(newBook, cancellationToken);
            _entityUnitOfWork.Commit();

            return ResultFactory.Ok();
        }
    }
}