using Edri.Domain.DomainEvents;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Infrastructure.Database;
using Edri.Infrastructure.EventSourcing;
using Edri.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Edri.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string migrationsAssemblyName,
        string connectionString)
    {
        // Add event store db context
        services.AddDbContext<EventStoreDbContext>(
            options =>
            {
                // ✅ CAMBIO 1: PostgreSQL
                options.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly(migrationsAssemblyName));
            });

        services.AddDbContext<DomainNotificationStoreDbContext>(
            options =>
            {
                // ✅ CAMBIO 2: PostgreSQL
                options.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly(migrationsAssemblyName));
            });

        // Core Infra
        services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
        services.AddScoped<IEventStoreContext, EventStoreContext>();
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        services.AddScoped<IDomainEventStore, DomainEventStore>();
        services.AddScoped<IMediatorHandler, InMemoryBus>();

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IProvinceRepository, ProvinceRepository>();
        services.AddScoped<IDistrictRepository, DistrictRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<ITariffRepository, TariffRepository>();
        services.AddScoped<ISupplyRepository, SupplyRepository>();
        services.AddScoped<IMeterRepository, MeterRepository>();
        return services;
    }
}