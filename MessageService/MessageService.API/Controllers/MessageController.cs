using MessageService.Application.Interfaces.Strategy;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly INotificationSenderStrategy _notificationSenderStrategy;

        public MessageController(INotificationSenderStrategy notificationSenderStrategy)
        {
            _notificationSenderStrategy = notificationSenderStrategy;
        }

        [HttpGet]
        public async Task<IActionResult> SendConsoleNotification(string message)
        {
            await _notificationSenderStrategy.Send(message, SendlerType.Console);

            return Ok(message);
        }
    }
}
