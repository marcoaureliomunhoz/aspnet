using System;
using System.Threading;
using BookShop.Infra.Messaging.Interfaces;
using BookShop.Infra.Messaging.Models;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace BookShop.Infra.Messaging.Services
{
    public delegate void OnError(Exception ex);
    public delegate void OnMessage<TValue>(TValue value);
    public delegate void OnMessage(string value, Exception ex);

    public class MessageConsumer : IDisposable, IMessageConsumer
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private bool _active = false;
        private CancellationTokenSource _cancellationTokenSource;

        public MessageConsumer(IConsumer<Ignore, string> consumer)
        {
            _consumer = consumer;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Consume<TValue>(
            MessageConsumerConfiguration configuration,
            OnMessage<TValue> onMessageValue,
            OnMessage onMessageText,
            OnError onError)
        {
            System.Console.WriteLine($"Start consumer {configuration.Topic}");

            _consumer.Subscribe(configuration.Topic);

            try
            {
                long consumeCount = 1;
                _active = true;
                while (_active)
                {
                    System.Console.WriteLine($"Consume {consumeCount}...");
                    var result = _consumer.Consume(_cancellationTokenSource.Token);
                    if (result != null)
                    {
                        try
                        {
                            var value = JsonConvert.DeserializeObject<TValue>(result.Message.Value);
                            onMessageValue?.Invoke(value);
                        }
                        catch (Exception convertException)
                        {
                            onMessageText?.Invoke(result.Message.Value, convertException);
                        }
                    }
                    consumeCount++;
                }
            }
            catch (OperationCanceledException)
            {
                System.Console.WriteLine($"Consumer {configuration.Topic} canceled");
                _consumer.Close();
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }

            Dispose();
            System.Console.WriteLine($"Finish consumer {configuration.Topic}");
        }

        public void Dispose()
        {
            System.Console.WriteLine($"Disposing consumer...");
            try
            {
                _consumer.Dispose();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Dispose consumer error: {ex.Message}");
            }
        }

        public void Deactive()
        {
            System.Console.WriteLine("Deactiving message consumer...");
            _active = false;
            _cancellationTokenSource.Cancel();
        }
    }
}