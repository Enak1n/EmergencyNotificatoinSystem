using EmergencyNotificationSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmergencyNotificationSystem.Infrastructure.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<NotificationEntity>
    {
        public void Configure(EntityTypeBuilder<NotificationEntity> builder)
        {
            builder.ToTable("Notifications");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.Type).HasConversion(
                                                v => v.ToString(),
                                                v => (NotificationType)Enum.Parse(typeof(NotificationType), v));

            builder.Property(x => x.Status).HasConversion(
                                    v => v.ToString(),
                                    v => (NotificationStatus)Enum.Parse(typeof(NotificationStatus), v));

            builder.Property(x => x.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp with time zone");
        }
    }
}
