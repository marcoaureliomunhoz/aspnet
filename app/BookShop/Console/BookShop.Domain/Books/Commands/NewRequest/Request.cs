using BookShop.Domain.Common;
using MediatR;

namespace BookShop.Domain.Books.Commands.NewRequest
{
    public class Request
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}