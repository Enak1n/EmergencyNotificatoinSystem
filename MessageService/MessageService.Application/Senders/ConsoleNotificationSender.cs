using EmergencyNotificationSystem.Domain.Interfaces.Services.Strategy;

namespace EmergencyNotificationSystem.Application.Senders
{
    public class ConsoleNotificationSender : ISendlerType
    {
        public SendlerType Sendler => SendlerType.Console;

        public async Task Send(string notification)
        {
            Console.WriteLine($"Отправлено {notification}");
        }
    }
}
