namespace EmergencyNotificationSystem.Infrastructure.Entities
{
    public class NotificationUserEntity
    {
        public Guid NotificationId { get; set; }
        public Guid UserId { get; set; }

        public NotificationStatus NotificationStatus { get; set; }

        public NotificationEntity Notification { get; set; }
        public UserEntity User { get; set; }
    }
}
