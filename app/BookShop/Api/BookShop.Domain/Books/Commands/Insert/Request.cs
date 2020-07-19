using BookShop.Domain.Common;
using MediatR;

namespace BookShop.Domain.Books.Commands.Insert
{
    public class Request : Validatable, IRequest<Result<bool>>
    {
        public string Title { get; set; }
        public int Stock { get; set; }

        public override void Validate()
        {
            if (Title.IsNullOrEmpty()) 
                AddNotification(nameof(Title), "Title is invalid");
            
            if (Stock.isLessThanZero())
                AddNotification(nameof(Title), "Stock is invalid");
        }
    }
}