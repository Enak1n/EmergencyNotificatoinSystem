namespace EmergencyNotificationSystem.Domain.Models
{
    public class BaseModel
    {
        public Guid Id { get; protected set; }
        public DateTimeOffset CreatedDate { get; protected set; }
    }
}
