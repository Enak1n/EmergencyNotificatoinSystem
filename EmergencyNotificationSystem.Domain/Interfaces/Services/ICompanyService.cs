using EmergencyNotificationSystem.Domain.Models.CompayAggregate;

namespace EmergencyNotificationSystem.Domain.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<List<Company>> GetAll();
        Task<Company> GetById(Guid id);
        Task Create(Guid id, DateTime dateOfCreation, string name);
        Task Delete(Guid id);
    }
}
