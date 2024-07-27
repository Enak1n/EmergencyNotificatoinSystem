using EmergencyNotificationSystem.Domain.Models.UserAggregate;

namespace EmergencyNotificationSystem.Infrastructure.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<CompanyEntity> Companies { get; set; }
    }
}
