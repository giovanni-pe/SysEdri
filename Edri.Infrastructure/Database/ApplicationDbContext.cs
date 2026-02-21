using System.Linq;
using Edri.Domain.Entities;
using Edri.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Edri.Infrastructure.Database;

public partial class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Province> Provinces { get; set; } = null!;

    public DbSet<Domain.Entities.District> Districts { get; set; } = null!;

    public DbSet<Customer> Customers { get; set; } = null!;

    public DbSet<Company> Companies { get; set; } = null!;
    
    public DbSet<Branch> Branches { get; set; } = null!;
    public DbSet<Tariff> Tariffs { get; set; } = null!;
    public DbSet<Supply> Supplies { get; set; } = null!;
    public DbSet<Meter> Meters { get; set; } = null!;


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            if (entity.ClrType.GetProperty(DbContextUtility.IsDeletedProperty) is not null)
            {
                builder.Entity(entity.ClrType)
                    .HasQueryFilter(DbContextUtility.GetIsDeletedRestriction(entity.ClrType));
            }
        }

        base.OnModelCreating(builder);

        ApplyConfigurations(builder);

        // Make referential delete behaviour restrict instead of cascade for everything
        foreach (var relationship in builder.Model.GetEntityTypes()
                     .SelectMany(x => x.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    private static void ApplyConfigurations(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new TenantConfiguration());
        builder.ApplyConfiguration(new DepartmentConfiguration());
        builder.ApplyConfiguration(new ProvinceConfiguration());
        builder.ApplyConfiguration(new DistrictConfiguration());
        builder.ApplyConfiguration(new CustomerConfiguration());
        builder.ApplyConfiguration(new CompanyConfiguration());
        builder.ApplyConfiguration(new BranchConfiguration());
        builder.ApplyConfiguration(new TariffConfiguration());
        builder.ApplyConfiguration(new SupplyConfiguration());
        builder.ApplyConfiguration(new MeterConfiguration());
    }
}