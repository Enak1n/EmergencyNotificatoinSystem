using AutoMapper;
using EmergencyNotificationSystem.Domain.Exceptions;
using EmergencyNotificationSystem.Domain.Interfaces.Repositories;
using EmergencyNotificationSystem.Domain.Models.UserAggregate;
using EmergencyNotificationSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmergencyNotificationSystem.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public UserRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(User user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                Name = user.Name,
                Address = user.Address,
                CreatedDate = user.CreatedDate
            };

            await _context.Users.AddAsync(userEntity);
        }

        public async Task Delete(Guid id)
        {
            await _context.Users.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();

            return _mapper.Map<List<User>>(users);
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new EntityNotFoundException($"User with id {id} not found!");

            return _mapper.Map<User>(user);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
