using EmergencyNotificationSystem.Domain.Interfaces.Services.Strategy;

namespace EmergencyNotificationSystem.Application.Senders.Sendlers
{
    public class ConsoleSendler : ISendlerType
    {
        public SendlerType Sendler => SendlerType.Console;

        public async Task Send(string notification)
        {
            Console.WriteLine("Отправлено");
        }
    }
}
