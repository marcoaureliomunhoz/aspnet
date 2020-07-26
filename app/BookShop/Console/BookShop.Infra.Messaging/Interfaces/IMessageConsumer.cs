using System.Threading.Tasks;
using BookShop.Infra.Messaging.Models;
using BookShop.Infra.Messaging.Services;

namespace BookShop.Infra.Messaging.Interfaces
{
    public interface IMessageConsumer
    {
        Task Consume<TValue>(MessageConsumerConfiguration configuration);
        void Deactive();
    }
}