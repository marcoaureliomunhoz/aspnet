using MediatR;

namespace BookShop.Domain.Books.Commands.NewRequest
{
    public class Notification : INotification
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}