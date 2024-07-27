﻿using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;

namespace EmergencyNotificationSystem.Domain.Interfaces.Services
{
    public interface INotificationService
    {
        Task<List<Notification>> GetAll();
        Task<Notification> GetById(Guid id);
        Task CreateNotification(Guid id, DateTime dateOfCreation, string message, NotificationType notificationType);
    }
}
