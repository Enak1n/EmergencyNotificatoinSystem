namespace EmergencyNotificationSystem.Domain.Models.NotificationAggregate
{
    public class Notification : BaseModel
    {
        public string Message { get; private set; }
        public NotificationType Type { get; private set; }

        private Notification(Guid id, DateTimeOffset dateOfCreation, string message, NotificationType notificationType)
        {
            Id = id;
            CreatedDate = dateOfCreation;
            Message = message;
            Type = notificationType;
        }

        public static Notification Create(Guid id, DateTimeOffset dateOfCreation, string message, NotificationType notificationType)
        {
            if(string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException($"Message can't be null!");
            }

            if(dateOfCreation < DateTimeOffset.UtcNow)
            {
                throw new ArgumentException("Incorrect date");
            }

            return new Notification(id, dateOfCreation, message, notificationType);
        }
    }
}
