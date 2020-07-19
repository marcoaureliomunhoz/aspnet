using MediatR;

namespace BookShop.Domain.Books.Commands.NewRequestApproved
{
    public class Notification : INotification
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}