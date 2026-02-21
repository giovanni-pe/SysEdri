using Edri.Domain.DomainNotifications;
using Edri.Infrastructure.Configurations.EventSourcing;
using Microsoft.EntityFrameworkCore;

namespace Edri.Infrastructure.Database;

public class DomainNotificationStoreDbContext : DbContext
{
    public virtual DbSet<StoredDomainNotification> StoredDomainNotifications { get; set; } = null!;

    public DomainNotificationStoreDbContext(DbContextOptions<DomainNotificationStoreDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new StoredDomainNotificationConfiguration());
    }
}