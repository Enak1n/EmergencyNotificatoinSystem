using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;

namespace EmergencyNotificationSystem.Domain.Interfaces.Services.Strategy
{
    public interface ISendlerType
    {
        SendlerType Sendler { get; }

        Task Send(Notification notification);
    }
}
