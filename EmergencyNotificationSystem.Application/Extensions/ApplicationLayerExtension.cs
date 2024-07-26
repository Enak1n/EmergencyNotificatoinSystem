using EmergencyNotificationSystem.Application.Services;
using EmergencyNotificationSystem.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmergencyNotificationSystem.Application.Extensions
{
    public static class ApplicationLayerExtension
    {
        public static IServiceCollection AddApplicationService(
                this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
            return services;
        }
    }
}
