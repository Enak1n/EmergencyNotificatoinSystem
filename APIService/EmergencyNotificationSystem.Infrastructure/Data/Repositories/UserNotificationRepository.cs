using AutoMapper;
using EmergencyNotificationSystem.Domain.Exceptions;
using EmergencyNotificationSystem.Domain.Interfaces.Repositories;
using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;
using EmergencyNotificationSystem.Infrastructure.Entities;

namespace EmergencyNotificationSystem.Infrastructure.Data.Repositories
{
    public class UserNotificationRepository : INotificationUsersRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public UserNotificationRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Notification> ChangeStatus(Guid id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if(notification == null)
            {
                throw new EntityNotFoundException($"Notification with id {id} not found!");
            }

            var notificationUser = await _context.NotificationUsers.FindAsync(notification.Id);

            notification.Status = NotificationStatus.Submitted;

            await _context.SaveChangesAsync();
            return _mapper.Map<Notification>(notification);
        }
    }
}
