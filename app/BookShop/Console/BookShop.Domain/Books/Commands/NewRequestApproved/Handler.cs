using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BookShop.Domain.Books.Commands.NewRequestApproved
{
    public class Handler : INotificationHandler<Notification>
    {
        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            //grava no topico approved

            await Task.CompletedTask;
        }
    }
}