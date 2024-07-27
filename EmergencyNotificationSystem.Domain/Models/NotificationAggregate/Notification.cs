namespace EmergencyNotificationSystem.Domain.Models.NotificationAggregate
{
    public class Notification : BaseModel
    {
        public string Message { get; private set; }
        public NotificationType Type { get; private set; }

        private Notification()
        {
            
        }

        private Notification(Guid id, DateTime dateOfCreation, string message, NotificationType notificationType)
        {
            Id = id;
            CreatedDate = dateOfCreation;
            Message = message;
            Type = notificationType;
        }

        public static Notification Create(Guid id, DateTime dateOfCreation, string message, NotificationType notificationType)
        {
            if(string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException($"Message can't be null!");
            }

            return new Notification(id, dateOfCreation, message, notificationType);
        }
    }
}
