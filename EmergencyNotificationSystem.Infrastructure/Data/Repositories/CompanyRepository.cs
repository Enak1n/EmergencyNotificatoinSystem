using AutoMapper;
using EmergencyNotificationSystem.Domain.Exceptions;
using EmergencyNotificationSystem.Domain.Interfaces.Repositories;
using EmergencyNotificationSystem.Domain.Models.CompayAggregate;
using EmergencyNotificationSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmergencyNotificationSystem.Infrastructure.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public CompanyRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(Company company)
        {
            var companyEntity = new CompanyEntity
            {
                Id = company.Id,
                Name = company.Name,
                CreatedDate = company.CreatedDate,
            };

            await _context.Companies.AddAsync(companyEntity);
        }

        public async Task Delete(Guid id)
        {
            await _context.Companies.AsNoTracking().Where(c => c.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<Company>> GetAll()
        {
            var companies =  await _context.Companies.ToListAsync();
            return _mapper.Map<List<Company>>(companies);
        }

        public async Task<Company> GetById(Guid id)
        {
            var companyEntity = await _context.Companies.FindAsync(id);

            if (companyEntity == null)
                throw new EntityNotFoundException($"Company with {id} not found!");

            return _mapper.Map<Company>(companyEntity);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public Task Update(Company company)
        {
            throw new NotImplementedException();
        }
    }
}
