using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BookShop.Infra.Messaging.Interfaces;
using BookShop.Infra.Messaging.Models;
using BookShop.Infra.Messaging.Exceptions;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace BookShop.Infra.Messaging.Services
{
    public delegate void OnError(Exception ex);
    public delegate void OnMessage<TValue>(TValue value);
    public delegate void OnMessage(string value, Exception ex);

    public class MessageConsumer : IDisposable, IMessageConsumer
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly IConsumer<Ignore, string> _consumer;
        private bool _active = false;
        private CancellationTokenSource _cancellationTokenSource;
        private IServiceCollection _services;

        public MessageConsumer(
            ConsumerConfig consumerConfig,
            IConsumer<Ignore, string> consumer,
            IServiceCollection services)
        {
            _consumerConfig = consumerConfig;
            _consumer = consumer;
            _services = services;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task Consume<TValue>(MessageConsumerConfiguration configuration)
        {
            System.Console.WriteLine($"Start consumer {configuration.Topic}");

            _consumer.Subscribe(configuration.Topic);

            var serviceProvider = _services.BuildServiceProvider();

            try
            {
                long consumeCount = 1;
                _active = true;
                while (_active)
                {
                    System.Console.WriteLine($"Consume {consumeCount}");
                    var resultConsume = _consumer.Consume(_cancellationTokenSource.Token);
                    if (resultConsume != null)
                    {
                        var offset = resultConsume?.Offset.Value ?? 0;
                        System.Console.WriteLine($" Offset {offset}");
                        var handler = serviceProvider.GetService<IMessageHandler<TValue>>();
                        if (handler != null)
                        {
                            try
                            {
                                var value = JsonConvert.DeserializeObject<TValue>(resultConsume.Message.Value);
                                var resultHandle = await handler.HandleMessage(value);
                                if (!_consumerConfig.EnableAutoCommit == true && resultHandle)
                                {
                                    _consumer.Commit(resultConsume);
                                }
                            }
                            catch (Exception convertException)
                            {
                                await handler.HandleException(convertException);
                            }
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
                var errorHandler = serviceProvider.GetService<IMessageErrorHandler>();
                if (errorHandler == null) throw new MessageConsumerException(configuration.Topic, ex);
                await errorHandler.Handle(ex);
            }

            Dispose();
            System.Console.WriteLine($"Finish consumer {configuration.Topic}");
        }

        // private Task<CommittedOffsets> CommitAsync(Message message)
        // {
        //     if (message.Error.Code != ErrorCode.NoError)
        //     {
        //         throw new InvalidOperationException("Must not commit offset for errored message");
        //     }
        //     return CommitAsync(new[] { new TopicPartitionOffset(message.TopicPartition, message.Offset + 1) });
        // }

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