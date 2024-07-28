using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace MessageBroker.Kafka.Lib
{
    public sealed class MessageBus<T> : IDisposable
    {
        private readonly IProducer<int, T> _producer;
        private IConsumer<int, T> _consumer;

        private readonly ProducerConfig _producerConfig;
        private readonly ConsumerConfig _consumerConfig;

        public MessageBus() : this("localhost:9092") { }

        public MessageBus(string host)
        {

            _producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                Acks = Acks.All
            };

            _consumerConfig = new ConsumerConfig
            {
                GroupId = "custom-group",
                BootstrapServers = host
            };

            _producer = new ProducerBuilder<int, T>(_producerConfig)
                .Build();
        }

        public async Task SendMessage(string topic, T message)
        {
            var newMessage = new Message<int, T>
            {
                Key = 1,
                Value = message,
                Headers = null
            };

            await _producer.ProduceAsync(topic, newMessage);
        }

        public void ConsumeMessage(string topic)
        {
            _consumer.Subscribe(topic);
            while (true)
            {
                try
                {
                    var messageFetchedfromTopic = _consumer.Consume();
                    Console.WriteLine(messageFetchedfromTopic.Message.Value);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Kafka Consumer on Topic {topic} failed to consume message {exception.Message}");
                }
            }
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