using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BookShop.Infra.Net.Models;
using BookShop.Infra.Net.Interfaces;
using System.Linq;
using BookShop.Domain.Common;

namespace BookShop.Domain.Books.Commands.NewRequest
{
    public class Handler : INotificationHandler<NewRequest.Notification>
    {
        private readonly IMediator _mediator;
        private readonly WebServicesConfiguration _webServices;
        private readonly IHttpService _httpService;
        private string _urlBase;

        public Handler(
            IMediator mediator,
            WebServicesConfiguration webServices,
            IHttpService httpService)
        {
            _mediator = mediator;
            _webServices = webServices;
            _httpService = httpService;
            LoadWebService();
        }

        private void LoadWebService()
        {
            var webService = _webServices.List.FirstOrDefault();
            var service = webService.Services.FirstOrDefault();
            var action = service.Actions.FirstOrDefault();
            _urlBase = webService.Url + "/" + service.Url + "/" + action.Url;
        }
        
        public async Task Handle(NewRequest.Notification notification, CancellationToken cancellationToken)
        {
            System.Console.WriteLine($"  Post {_urlBase}");
            var response = await _httpService.PostAsync<NewRequest.Notification, Result<bool>>(
                _urlBase,
                notification
            );

            if (response != null && !response.HasMessage && response.Data)
            {
                System.Console.WriteLine("      Publish NewRequestApproved notification...");
                await _mediator.Publish(new NewRequestApproved.Notification
                {
                    BookId = notification.BookId,
                    Quantity = notification.Quantity
                }, cancellationToken);
            }
            else
            {
                System.Console.WriteLine("      Publish NewRequestDisapproved notification...");
                await _mediator.Publish(new NewRequestDisapproved.Notification
                {
                    BookId = notification.BookId,
                    Quantity = notification.Quantity
                }, cancellationToken);
            }
        }
    }
}