using MessageBroker.Kafka.Lib;
using MessageService.API.Controllers;
using MessageService.API.Models;
using MessageService.API.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace MessageService.API.Services
{
    public class ConsumerService : BackgroundService
    {
        private readonly MessageBus _messageBus;
        private readonly MessageController _messageController;
        private readonly KafkaSettings _kafkaSettings;

        public ConsumerService(MessageBus messageBus, MessageController messageController, IOptions<KafkaSettings> kafkaSettings)
        {
            _messageBus = messageBus;
            _messageController = messageController;
            _kafkaSettings = kafkaSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = await _messageBus.ConsumeMessage(_kafkaSettings.NotificationTopic);

                if (message == null)
                {
                    await Task.Delay(1000);
                    continue;
                }

                var notification = JsonSerializer.Deserialize<NotificationMessage>(message);
                await _messageController.SendConsoleNotification(notification);
            }

        }
    }
}
