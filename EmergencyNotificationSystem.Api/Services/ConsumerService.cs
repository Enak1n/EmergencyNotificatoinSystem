
using MessageBroker.Kafka.Lib;

namespace EmergencyNotificationSystem.Api.Services
{
    public class ConsumerService : BackgroundService
    {
        private readonly MessageBus<string> _messageBus;

        public ConsumerService(MessageBus<string> messageBus)
        {
            _messageBus = messageBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await _messageBus.ConsumeMessage("hui");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can't consume message!");
            }
        }
    }
}
