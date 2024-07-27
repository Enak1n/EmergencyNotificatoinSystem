using EmergencyNotificationSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmergencyNotificationSystem.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.ComplexProperty(c => c.Address, b =>
            {
                b.IsRequired();
                b.Property(p => p.Street).HasColumnName("Street");
                b.Property(p => p.City).HasColumnName("City");
                b.Property(p => p.House).HasColumnName("House");
            });

            builder.HasMany(e => e.Companies).WithMany(e => e.Users);
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp with time zone");
        }
    }
}
