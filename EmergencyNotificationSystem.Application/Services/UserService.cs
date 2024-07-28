using EmergencyNotificationSystem.Domain.Interfaces.Repositories;
using EmergencyNotificationSystem.Domain.Interfaces.Services;
using EmergencyNotificationSystem.Domain.Models.UserAggregate;

namespace EmergencyNotificationSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
