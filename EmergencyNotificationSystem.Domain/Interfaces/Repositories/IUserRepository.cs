using EmergencyNotificationSystem.Domain.Models.UserAggregate;

namespace EmergencyNotificationSystem.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task Create(User user);
        Task Delete(Guid id);
        Task<User> GetById(Guid id);
        Task Update(User user);
        Task<int> SaveChanges();
    }
}
