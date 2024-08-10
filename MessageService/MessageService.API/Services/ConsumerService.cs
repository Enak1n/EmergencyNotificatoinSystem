using MessageBroker.Kafka.Lib;
using MessageService.API.Controllers;

namespace MessageService.API.Services
{
    public class ConsumerService : BackgroundService
    {
        private readonly MessageBus _messageBus;
        private readonly MessageController _messageController;

        public ConsumerService(MessageBus messageBus, MessageController messageController)
        {
            _messageBus = messageBus;
            _messageController = messageController;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = await _messageBus.ConsumeMessage("hui");

                if (message == null)
                {
                    await Task.Delay(1000);
                    continue;
                }

                await _messageController.SendConsoleNotification(message);
            }

        }
    }
}
