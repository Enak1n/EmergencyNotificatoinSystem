using MessageBroker.Kafka.Lib;
using MessageService.API.Models;
using MessageService.API.Options;
using MessageService.Application.Interfaces.Strategy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace MessageService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly INotificationSenderStrategy _notificationSenderStrategy;
        private readonly MessageBus _messageBus;
        private readonly KafkaSettings _kafkaSettings;

        public MessageController(INotificationSenderStrategy notificationSenderStrategy,
            MessageBus messageBus, IOptions<KafkaSettings> kafkaSettings)
        {
            _notificationSenderStrategy = notificationSenderStrategy;
            _messageBus = messageBus;
            _kafkaSettings = kafkaSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> SendConsoleNotification(NotificationMessage notification)
        {
            await _notificationSenderStrategy.Send(notification.Message, SendlerType.Console);

            var notificationMessage = JsonSerializer.Serialize(notification);
            await _messageBus.SendMessage(_kafkaSettings.SendTopic, notificationMessage);    
            return Ok(notification);
        }
    }
}
