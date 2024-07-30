using EmergencyNotificationSystem.Domain.Interfaces.Services;
using EmergencyNotificationSystem.Domain.Interfaces.Services.Strategy;
using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;
using EmergencyNotificationSystem.Infrastructure.Dto;
using MessageBroker.Kafka.Lib;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyNotificationSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationSenderStrategy _senderStrategy;
        private readonly MessageBus<string> _messageBus;

        public NotificationController(INotificationService notificationService, INotificationSenderStrategy senderStrategy, MessageBus<string> messageBus)
        {
            _notificationService = notificationService;
            _senderStrategy = senderStrategy;
            _messageBus = messageBus;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationService.GetAll();

            await _senderStrategy.Send(notifications.FirstOrDefault(), SendlerType.Console);

            await _messageBus.SendMessage("hui", "Отправлено");
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
            await _notificationService.CreateNotification(Guid.NewGuid(), DateTime.UtcNow, notificationDto.Message, notificationDto.NotificationType);

            return Created();
        }
    }
}
