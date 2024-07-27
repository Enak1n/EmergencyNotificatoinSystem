using EmergencyNotificationSystem.Domain.Models.UserAggregate;

namespace EmergencyNotificationSystem.Domain.Models.CompayAggregate
{
    public class Company : BaseModel
    {

        private readonly List<User> _users = new();
        public string Name { get; private set; }
        public IReadOnlyCollection<User>? Users => _users;

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

        public void AddUser(User user) => _users.Add(user);
    }
}
