using System;
using System.Threading.Tasks;

namespace BookShop.Infra.Messaging.Interfaces
{
    public interface IMessageHandler<TMessage>
    {
        Task<bool> HandleMessage(TMessage message);
        Task HandleException(Exception ex);
    }
}