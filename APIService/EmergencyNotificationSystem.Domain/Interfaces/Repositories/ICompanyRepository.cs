using EmergencyNotificationSystem.Domain.Models.CompayAggregate;

namespace EmergencyNotificationSystem.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAll();
        Task Create(Company company);
        Task Delete(Guid id);
        Task<Company> GetById(Guid id);
        Task Update(Company company);
        Task<int> SaveChanges();
    }
}
