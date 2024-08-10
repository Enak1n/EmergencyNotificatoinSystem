using EmergencyNotificationSystem.Domain.Models.UserAggregate;

namespace EmergencyNotificationSystem.Domain.Models.CompayAggregate
{
    public class Company : BaseModel
    {
        public string Name { get; private set; }

        private Company()
        {

        }

        private Company(Guid id, DateTime dateOfCreation, string name)
        {
            Id = id;
            CreatedDate = dateOfCreation;
            Name = name;
        }

        public static Company Create(Guid id, DateTime dateOfCreation, string name)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name can't  be null");

            return new Company(id, dateOfCreation, name);
        }
    }
}
