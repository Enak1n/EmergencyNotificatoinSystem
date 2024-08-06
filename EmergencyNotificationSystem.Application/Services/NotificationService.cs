using EmergencyNotificationSystem.Domain.Interfaces.Repositories;
using EmergencyNotificationSystem.Domain.Interfaces.Services;
using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;

namespace EmergencyNotificationSystem.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserService _userRepository;

        public NotificationService(INotificationRepository notificationRepository, IUserService userRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        public async Task<Notification> CreateNotification(Guid id, DateTime dateOfCreation, string message, NotificationType notificationType)
        {
            var notification = Notification.Create(id, dateOfCreation, message, notificationType);
            var users = await _userRepository.GetAll();

            notification.AddUsers(users);

            await _notificationRepository.Create(notification);
            await _notificationRepository.SaveChanges();

            return notification;
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
