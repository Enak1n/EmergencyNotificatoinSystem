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

        public async Task Delete(Guid id)
        {
            await _userRepository.Delete(id);
            await _userRepository.SaveChanges();
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);

            return user;
        }
    }
}
