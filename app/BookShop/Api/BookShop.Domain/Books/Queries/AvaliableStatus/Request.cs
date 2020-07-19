using BookShop.Domain.Common;
using MediatR;

namespace BookShop.Domain.Books.Queries.AvaliableStatus
{
    public class Request : Validatable, IRequest<Result<bool>>
    {
        public int BookId { get; set; }

        public override void Validate()
        {
            if (!BookId.isGraterThanZero())
                AddNotification(nameof(BookId), "Book identification is invalid");
        }
    }
}