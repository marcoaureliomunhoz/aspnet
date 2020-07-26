using System.Threading.Tasks;
using Confluent.Kafka;
using BookShop.Infra.Messaging.Models;

namespace BookShop.Infra.Messaging.Interfaces
{
    public interface IMessageProducer
    {
         Task<DeliveryResult<Null, string>> ProduceAsync<TValue>(
            MessageProducerConfiguration messageConfiguration,
            TValue value);
    }
}