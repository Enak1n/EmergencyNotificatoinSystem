using EmergencyNotificationSystem.Domain.Exceptions;
using EmergencyNotificationSystem.Domain.Interfaces.Repositories;
using EmergencyNotificationSystem.Domain.Interfaces.Services;
using EmergencyNotificationSystem.Domain.Models.CompayAggregate;

namespace EmergencyNotificationSystem.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task Create(Guid id, DateTime dateOfCreation, string name)
        {
            var company = Company.Create(id, dateOfCreation, name);
            await _companyRepository.Create(company);
            await _companyRepository.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            var company = _companyRepository.GetById(id);

            if (company == null)
                throw new EntityNotFoundException($"Company with {id} not found!");

            await _companyRepository.Delete(id);
        }

        public async Task<List<Company>> GetAll()
        {
            return await _companyRepository.GetAll();
        }

        public async Task<Company> GetById(Guid id)
        {
            return await _companyRepository.GetById(id);
        }
    }
}
