using EmergencyNotificationSystem.Domain.Interfaces.Repositories;
using EmergencyNotificationSystem.Domain.Interfaces.Services;
using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;

namespace EmergencyNotificationSystem.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task CreateNotification(Guid id, DateTime dateOfCreation, string message, NotificationType notificationType)
        {
            var notification = Notification.Create(id, dateOfCreation, message, notificationType);
            await _notificationRepository.Create(notification);
            await _notificationRepository.SaveChanges();
        }

        public async Task<List<Notification>> GetAll()
        {
            return await _notificationRepository.GetAll();
        }

        public async Task<Notification> GetById(Guid id)
        {
            return await _notificationRepository.GetById(id);
        }
    }
}
