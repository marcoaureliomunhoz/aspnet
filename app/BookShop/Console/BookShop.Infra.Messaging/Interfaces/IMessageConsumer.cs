using System.Threading;
using BookShop.Infra.Messaging.Models;
using BookShop.Infra.Messaging.Services;

namespace BookShop.Infra.Messaging.Interfaces
{
    public interface IMessageConsumer
    {
         void Consume<TValue>(
            MessageConsumerConfiguration configuration,
            OnMessage<TValue> onMessage,
            OnMessage onMessageText,
            OnError onError);
        void Deactive();
    }
}