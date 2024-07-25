using AutoMapper;
using EmergencyNotificationSystem.Domain.Models.NotificationAggregate;
using EmergencyNotificationSystem.Infrastructure.Entities;

namespace EmergencyNotificationSystem.Infrastructure.Mappings
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings()
        {
            CreateMap<NotificationEntity, Notification>();
        }
    }
}
