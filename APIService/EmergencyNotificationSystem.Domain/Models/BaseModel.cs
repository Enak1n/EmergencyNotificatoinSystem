namespace EmergencyNotificationSystem.Domain.Models
{
    public class BaseModel
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
    }
}
