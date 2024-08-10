using EmergencyNotificationSystem.Api.Options;
using EmergencyNotificationSystem.Domain.Interfaces.Services;
using EmergencyNotificationSystem.Infrastructure.Dto;
using MessageBroker.Kafka.Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EmergencyNotificationSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly MessageBus _messageBus;
        private readonly KafkaSettings _kafkaSettings;

        public NotificationController(INotificationService notificationService, MessageBus messageBus, IOptionsSnapshot<KafkaSettings> kafkaSettings)
        {
            _notificationService = notificationService;
            _messageBus = messageBus;
            _kafkaSettings = kafkaSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationService.GetAll();

            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var notification = await _notificationService.GetById(id);

            return Ok(notification);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNotificationDto notificationDto)
        {
            var notification = await _notificationService.CreateNotification(Guid.NewGuid(), DateTime.UtcNow, notificationDto.Message, notificationDto.NotificationType);

            var notificationMessage = new NotificationMessage(notification.Id, notification.Message);

            var notificationJson = JsonSerializer.Serialize(notificationMessage);

            await _messageBus.SendMessage(_kafkaSettings.NotificationTopic, notificationJson);
            return Created();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            await _notificationService.ChangeStatus(id);
            return Ok();
        }
    }
}
