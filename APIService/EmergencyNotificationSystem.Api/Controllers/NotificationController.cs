using EmergencyNotificationSystem.Domain.Interfaces.Services;
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
        private readonly MessageBus _messageBus;

        public NotificationController(INotificationService notificationService, MessageBus messageBus)
        {
            _notificationService = notificationService;
            _messageBus = messageBus;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationService.GetAll();

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
            var notification = await _notificationService.CreateNotification(Guid.NewGuid(), DateTime.UtcNow, notificationDto.Message, notificationDto.NotificationType);

            await _messageBus.SendMessage("hui", notification.Message);
            return Created();
        }
    }
}
