using EmergencyNotificationSystem.Domain.Models.UserAggregate;

namespace EmergencyNotificationSystem.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(Guid id);
        Task Register(User user);
        Task Login(string email, string password);
        Task Delete(Guid id);
    }
}
