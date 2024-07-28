namespace EmergencyNotificationSystem.Domain.Models.UserAggregate
{
    public class User : BaseModel
    {
        public string Name { get; private set; }
        public Address Address { get; private set; }

        public User(Guid id, DateTime dateOfCreation, string name, Address address)
        {
            Id = id;
            CreatedDate = dateOfCreation;
            Name = name;
            Address = address;
        }

        public static User Create(Guid id, DateTime dateOfCreation, string name, Address address)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name can't be null or empty");
            }

            return new User(id, dateOfCreation, name, address);
        }
    }
}
