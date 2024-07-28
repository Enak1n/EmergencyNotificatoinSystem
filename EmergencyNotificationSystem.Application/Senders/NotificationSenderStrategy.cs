using EmergencyNotificationSystem.Domain.Exceptions;
using EmergencyNotificationSystem.Domain.Interfaces.Services.Strategy;
using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;

namespace EmergencyNotificationSystem.Application.Senders
{
    public class NotificationSenderStrategy : INotificationSenderStrategy
    {
        private readonly IEnumerable<ISendlerType> _types;

        public NotificationSenderStrategy(IEnumerable<ISendlerType> types) => _types = types;

        public async Task Send(Notification notification, SendlerType sendler)
        {
            var sendlerType = _types.FirstOrDefault(x => x.Sendler == sendler);

            if (sendlerType == null)
            {
                throw new StrategyException(nameof(sendler));
            }

            await sendlerType.Send(notification);
        }
    }
}
