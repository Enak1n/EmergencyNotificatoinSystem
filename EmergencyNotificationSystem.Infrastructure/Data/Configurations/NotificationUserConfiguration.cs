using EmergencyNotificationSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyNotificationSystem.Infrastructure.Data.Configurations
{
    public class NotificationUserConfiguration : IEntityTypeConfiguration<NotificationUserEntity>
    {
        public void Configure(EntityTypeBuilder<NotificationUserEntity> builder)
        {
            builder.ToTable("UserNotification");
            builder.HasKey(x => new { x.NotificationId, x.UserId});

            builder.HasOne(t => t.Notification).WithMany().HasForeignKey(x => x.NotificationId);

            builder.HasOne(t => t.User).WithMany().HasForeignKey(x => x.UserId);

            builder.Property(x => x.NotificationStatus).HasConversion(
                                    v => v.ToString(),
                                    v => (NotificationStatus)Enum.Parse(typeof(NotificationStatus), v));
        }
    }
}
