using AutoMapper;
using EmergencyNotificationSystem.Domain.Exceptions;
using EmergencyNotificationSystem.Domain.Interfaces.Repositories;
using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;
using EmergencyNotificationSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmergencyNotificationSystem.Infrastructure.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public NotificationRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        public async Task Create(Notification notification)
        {
            var notificationUsers = new List<NotificationUserEntity>();
            var notificationEntity = new NotificationEntity
            {
                Id = notification.Id,
                Message = notification.Message,
                CreatedDate = notification.CreatedDate,
                Type = notification.Type,
            };

            await _context.Notifications.AddAsync(notificationEntity);

            foreach (var user in notification.Users)
            {
                var notificationUser = new NotificationUserEntity
                {
                    NotificationId = notification.Id,
                    UserId = user.Id,
                    Notification = notificationEntity,
                    NotificationStatus = NotificationStatus.NotSubmited
                };

                notificationUsers.Add(notificationUser);
            }

            await _context.NotificationUsers.AddRangeAsync(notificationUsers);
        }

        public async Task Delete(Guid id)
        {
            await _context.Notifications.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<Notification>> GetAll()
        {
            var notification = await _context.Notifications.AsNoTracking().ToListAsync();

            return _mapper.Map<List<Notification>>(notification);
        }

        public async Task<Notification> GetById(Guid id)
        {
            var notification =  await _context.Notifications
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (notification == null)
            {
                throw new EntityNotFoundException($"Notification with id {id} not found!");
            }

            var domainNotification = _mapper.Map<Notification>(notification);

            return domainNotification;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task Update(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
