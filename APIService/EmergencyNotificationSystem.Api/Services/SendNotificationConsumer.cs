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

        public IServiceProvider Services { get; }

        public SendNotificationConsumer(MessageBus messageBus, IOptions<KafkaSettings> kafkaSettings, IServiceProvider services)
        {
            _messageBus = messageBus;
            _kafkaSettings = kafkaSettings.Value;
            Services = services;
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

                using (var scope = Services.CreateScope())
                {
                    var scopedProcessingService =
                            scope.ServiceProvider
                                .GetRequiredService<NotificationController>();
                    await scopedProcessingService.ChangeStatus(notification.Id);
                }

                await Task.Delay(5000);
            }
        }
    }
}
