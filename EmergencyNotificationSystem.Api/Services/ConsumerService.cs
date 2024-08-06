using MessageBroker.Kafka.Lib;

namespace EmergencyNotificationSystem.Api.Services
{
    public class ConsumerService : BackgroundService
    {
        private readonly MessageBus _messageBus;

        public ConsumerService(MessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _messageBus.ConsumeMessage("hui", SendlerType.Console);
                Task.Delay(1000);
            }

        }
    }
}
