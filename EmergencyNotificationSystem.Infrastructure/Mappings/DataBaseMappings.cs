using AutoMapper;
using EmergencyNotificationSystem.Domain.Models.CompayAggregate;
using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;
using EmergencyNotificationSystem.Domain.Models.UserAggregate;
using EmergencyNotificationSystem.Infrastructure.Entities;

namespace EmergencyNotificationSystem.Infrastructure.Mappings
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings()
        {
            CreateMap<NotificationEntity, Notification>();
            CreateMap<UserEntity, User>();
            CreateMap<CompanyEntity, Company>();
        }
    }
}
