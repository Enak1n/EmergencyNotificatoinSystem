using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;

namespace EmergencyNotificationSystem.Domain.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetAll();
        Task Create(Notification notification);
        Task Delete(Guid id);
        Task<Notification> GetById(Guid id);
        Task Update(Notification notification);
        Task<int> SaveChanges();
    }
}
