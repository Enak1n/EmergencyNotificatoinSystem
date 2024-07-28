using EmergencyNotificationSystem.Domain.Interfaces.Services.Strategy;
using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;

namespace EmergencyNotificationSystem.Application.Senders
{
    public class ConsoleNotificationSender : ISendlerType
    {
        public SendlerType Sendler => SendlerType.Console;

        public async Task Send(Notification notification)
        {
            Console.WriteLine($"Отправлено {notification.Message}");
        }
    }
}
