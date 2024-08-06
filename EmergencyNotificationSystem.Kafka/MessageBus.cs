using Confluent.Kafka;
using Confluent.Kafka.Admin;
using EmergencyNotificationSystem.Domain.Interfaces.Services.Strategy;
using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;

namespace MessageBroker.Kafka.Lib
{
    public sealed class MessageBus : IDisposable
    {
        private readonly IProducer<int, string> _producer;
        private IConsumer<int, string> _consumer;

        private readonly ProducerConfig _producerConfig;
        private readonly ConsumerConfig _consumerConfig;

        private readonly INotificationSenderStrategy _notificationSenderStrategy;

        public MessageBus(string host, INotificationSenderStrategy senderStrategy)
        {
            _notificationSenderStrategy = senderStrategy;

            _producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                Acks = Acks.All
            };

            _consumerConfig = new ConsumerConfig
            {
                GroupId = "custom-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _producer = new ProducerBuilder<int, string>(_producerConfig)
                .Build();

            _consumer = new ConsumerBuilder<int, string>(_consumerConfig).Build();
        }

        public async Task SendMessage(string topic, string message)
        {
            var newMessage = new Message<int, string>
            {
                Key = 1,
                Value = message,
                Headers = null
            };

            await _producer.ProduceAsync(topic, newMessage);
        }

        public async Task ConsumeMessage(string topic, SendlerType sendlerType)
        {
            _consumer.Subscribe(topic);
            await Task.Yield();
            var messageFetchedFromTopic = _consumer.Consume(TimeSpan.FromSeconds(1));

            if (messageFetchedFromTopic == null)
                return;

            var notification = Notification.Create(Guid.NewGuid(), DateTime.UtcNow, messageFetchedFromTopic.Message.Value.ToString(), NotificationType.Alert);
            await _notificationSenderStrategy.Send(notification, sendlerType);

            Console.WriteLine($"Отправлено с брокера {messageFetchedFromTopic.Message.Value}");

            _consumer.Commit();
        }

        public static void Create(bool create)
        {
            if (create)
            {
                var kafkaAdminClientConfig = new AdminClientConfig
                {
                    BootstrapServers = "localhost:9092"
                };

                var adminClient = new AdminClientBuilder(kafkaAdminClientConfig)
                    .Build();
                CreateTopicWithAdminClient(adminClient);
            }
        }

        private static void CreateTopicWithAdminClient(IAdminClient adminClient)
        {
            try
            {
                adminClient.CreateTopicsAsync(new TopicSpecification[]
                {
                        new TopicSpecification
                        {
                            Name = "topic01",
                            ReplicationFactor = 2,
                            NumPartitions = 4
                        },
                        new TopicSpecification
                        {
                            Name = "topic02",
                            ReplicationFactor = 2,
                            NumPartitions = 4
                        }
                });
                Console.WriteLine("Topic(s) created Successfully");
                Console.WriteLine();
            }
            catch (CreateTopicsException e)
            {
                Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
            }
        }
        public void Dispose()
        {
            _producer?.Dispose();
            _consumer?.Dispose();
        }
    }
}