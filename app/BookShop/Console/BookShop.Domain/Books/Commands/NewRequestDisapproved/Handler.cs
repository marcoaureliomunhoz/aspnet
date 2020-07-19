using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BookShop.Domain.Books.Commands.NewRequestDisapproved
{
    public class Handler : INotificationHandler<NewRequestDisapproved.Notification>
    {
        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            //grava no topico disapproved

            await Task.CompletedTask;
        }
    }
}