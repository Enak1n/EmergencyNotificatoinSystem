using EmergencyNotificationSystem.Api.Controllers;
using EmergencyNotificationSystem.Api.Options;
using EmergencyNotificationSystem.Infrastructure.Dto;
using MessageBroker.Kafka.Lib;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EmergencyNotificationSystem.Api.Services
{
    public class SendNotificationConsumer : BackgroundService
    {
        private readonly MessageBus _messageBus;
        private readonly KafkaSettings _kafkaSettings;
        private readonly NotificationController _notificationController;

        public SendNotificationConsumer(MessageBus messageBus, IOptions<KafkaSettings> kafkaSettings, NotificationController notificationController)
        {
            _messageBus = messageBus;
            _kafkaSettings = kafkaSettings.Value;
            _notificationController = notificationController;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = await _messageBus.ConsumeMessage(_kafkaSettings.SendTopic);

                if(message == null)
                {
                    await Task.Delay(5000);
                    continue;
                }

                var notification = JsonSerializer.Deserialize<NotificationMessage>(message);

                await _notificationController.ChangeStatus(notification.Id);
                await Task.Delay(5000);
            }
        }
    }
}
