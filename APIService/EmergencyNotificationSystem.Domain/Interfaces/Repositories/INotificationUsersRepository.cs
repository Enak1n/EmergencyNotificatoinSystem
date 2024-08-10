using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;

namespace EmergencyNotificationSystem.Domain.Interfaces.Repositories
{
    // TODO Вынести в сервис сообщений.
    public interface INotificationUsersRepository
    {
        Task<Notification> ChangeStatus(Guid id);
    }
}
