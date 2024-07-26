using EmergencyNotificationSystem.Domain.Interfaces.Repositories;
using EmergencyNotificationSystem.Infrastructure.Data;
using EmergencyNotificationSystem.Infrastructure.Data.Repositories;
using EmergencyNotificationSystem.Infrastructure.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmergencyNotificationSystem.Infrastructure.Extensions
{
    public static class InfrastructureLayerExtension
    {
        public static IServiceCollection AddInfrastructureService(
           this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DataBaseMappings));
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddDbContext<Context>();
            return services;
        }

    }
}
