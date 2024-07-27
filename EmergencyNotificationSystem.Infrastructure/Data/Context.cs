using EmergencyNotificationSystem.Domain.Interfaces.Repositories;
using EmergencyNotificationSystem.Infrastructure.Data.Configurations;
using EmergencyNotificationSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmergencyNotificationSystem.Infrastructure.Data
{
    public class Context : DbContext, IUnitOfWork
    {
        public DbSet<NotificationEntity> Notifications => Set<NotificationEntity>();
        public DbSet<CompanyEntity> Companies => Set<CompanyEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..\\EmergencyNotificationSystem.Api"))
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder
                .UseNpgsql(configuration.GetConnectionString("ConnectionString"))
                .UseLoggerFactory(CreateLoggerFactory())
                .EnableSensitiveDataLogging();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
