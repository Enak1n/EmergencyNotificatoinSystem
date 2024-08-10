using EmergencyNotificationSystem.Domain.Models.UserAggregate;

namespace EmergencyNotificationSystem.Domain.Models.NotificationAggregate
{
    public class Notification : BaseModel
    {
        private readonly List<User> _users = new();

        public string Message { get; private set; }
        public NotificationType Type { get; private set; }
        public NotificationStatus Status { get; private set; }

        public IReadOnlyCollection<User> Users => _users;

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

        public void AddUsers(List<User> users) => _users.AddRange(users);
    }
}
