using Edri.Domain.Commands.Tenants.CreateTenant;
using Edri.Domain.Commands.Tenants.DeleteTenant;
using Edri.Domain.Commands.Tenants.UpdateTenant;
using Edri.Domain.Commands.Departments.CreateDepartment;
using Edri.Domain.Commands.Departments.DeleteDepartment;
using Edri.Domain.Commands.Departments.UpdateDepartment;
using Edri.Domain.Commands.Customers.CreateCustomer;
using Edri.Domain.Commands.Customers.DeleteCustomer;
using Edri.Domain.Commands.Customers.UpdateCustomer;
using Edri.Domain.Commands.Users.ChangePassword;
using Edri.Domain.Commands.Users.CreateUser;
using Edri.Domain.Commands.Users.DeleteUser;
using Edri.Domain.Commands.Users.LoginUser;
using Edri.Domain.Commands.Users.UpdateUser;
using Edri.Domain.EventHandler;
using Edri.Domain.EventHandler.Fanout;
using Edri.Domain.Interfaces;
using Edri.Shared.Events.Tenant;
using Edri.Shared.Events.Department;
using Edri.Shared.Events.Customer;
using Edri.Shared.Events.User;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Edri.Domain.Commands.Companies.CreateCompany;
using Edri.Domain.Commands.Companies.DeleteCompany;
using Edri.Domain.Commands.Companies.UpdateCompany;
using Edri.Shared.Events.Company;
using Edri.Domain.Commands.Branches.CreateBranch;
using Edri.Domain.Commands.Branches.DeleteBranch;
using Edri.Domain.Commands.Branches.UpdateBranch;
using Edri.Shared.Events.Branch;
using Edri.Domain.Commands.Tariffs.CreateTariff;
using Edri.Domain.Commands.Tariffs.DeleteTariff;
using Edri.Domain.Commands.Tariffs.UpdateTariff;
using Edri.Shared.Events.Tariff;
using Edri.Domain.Commands.Supplies.CreateSupply;
using Edri.Domain.Commands.Supplies.DeleteSupply;
using Edri.Domain.Commands.Supplies.UpdateSupply;
using Edri.Shared.Events.Supply;
using Edri.Domain.Commands.Meters.CreateMeter;
using Edri.Domain.Commands.Meters.DeleteMeter;
using Edri.Domain.Commands.Meters.UpdateMeter;
using Edri.Shared.Events.Meter;

namespace Edri.Domain.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        // User
        services.AddScoped<IRequestHandler<CreateUserCommand>, CreateUserCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateUserCommand>, UpdateUserCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteUserCommand>, DeleteUserCommandHandler>();
        services.AddScoped<IRequestHandler<ChangePasswordCommand>, ChangePasswordCommandHandler>();
        services.AddScoped<IRequestHandler<LoginUserCommand, string>, LoginUserCommandHandler>();

        // Tenant
        services.AddScoped<IRequestHandler<CreateTenantCommand>, CreateTenantCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateTenantCommand>, UpdateTenantCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteTenantCommand>, DeleteTenantCommandHandler>();

        // Department
        services.AddScoped<IRequestHandler<CreateDepartmentCommand>, CreateDepartmentCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateDepartmentCommand>, UpdateDepartmentCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteDepartmentCommand>, DeleteDepartmentCommandHandler>();

        // Customer
        services.AddScoped<IRequestHandler<CreateCustomerCommand>, CreateCustomerCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCustomerCommand>, UpdateCustomerCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteCustomerCommand>, DeleteCustomerCommandHandler>();
        // Company
        services.AddScoped<IRequestHandler<CreateCompanyCommand>, CreateCompanyCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCompanyCommand>, UpdateCompanyCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteCompanyCommand>, DeleteCompanyCommandHandler>();
        // Branch
        services.AddScoped<IRequestHandler<CreateBranchCommand>, CreateBranchCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateBranchCommand>, UpdateBranchCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteBranchCommand>, DeleteBranchCommandHandler>();
        // Tariff
        services.AddScoped<IRequestHandler<CreateTariffCommand>, CreateTariffCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateTariffCommand>, UpdateTariffCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteTariffCommand>, DeleteTariffCommandHandler>();

        // Supply
        services.AddScoped<IRequestHandler<CreateSupplyCommand>, CreateSupplyCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateSupplyCommand>, UpdateSupplyCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteSupplyCommand>, DeleteSupplyCommandHandler>();
        // Meter
        services.AddScoped<IRequestHandler<CreateMeterCommand>, CreateMeterCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateMeterCommand>, UpdateMeterCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteMeterCommand>, DeleteMeterCommandHandler>();

        return services;
    }

    public static IServiceCollection AddNotificationHandlers(this IServiceCollection services)
    {
        // Fanout
        services.AddScoped<IFanoutEventHandler, FanoutEventHandler>();

        // User
        services.AddScoped<INotificationHandler<UserCreatedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<UserUpdatedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<UserDeletedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<PasswordChangedEvent>, UserEventHandler>();

        // Tenant
        services.AddScoped<INotificationHandler<TenantCreatedEvent>, TenantEventHandler>();
        services.AddScoped<INotificationHandler<TenantUpdatedEvent>, TenantEventHandler>();
        services.AddScoped<INotificationHandler<TenantDeletedEvent>, TenantEventHandler>();

        // Department
        services.AddScoped<INotificationHandler<DepartmentCreatedEvent>, DepartmentEventHandler>();
        services.AddScoped<INotificationHandler<DepartmentUpdatedEvent>, DepartmentEventHandler>();
        services.AddScoped<INotificationHandler<DepartmentDeletedEvent>, DepartmentEventHandler>();

        // Customer
        services.AddScoped<INotificationHandler<CustomerCreatedEvent>, CustomerEventHandler>();
        services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
        services.AddScoped<INotificationHandler<CustomerDeletedEvent>, CustomerEventHandler>();
        // Company
        services.AddScoped<INotificationHandler<CompanyCreatedEvent>, CompanyEventHandler>();
        services.AddScoped<INotificationHandler<CompanyUpdatedEvent>, CompanyEventHandler>();
        services.AddScoped<INotificationHandler<CompanyDeletedEvent>, CompanyEventHandler>();
        // Branch
        services.AddScoped<INotificationHandler<BranchCreatedEvent>, BranchEventHandler>();
        services.AddScoped<INotificationHandler<BranchUpdatedEvent>, BranchEventHandler>();
        services.AddScoped<INotificationHandler<BranchDeletedEvent>, BranchEventHandler>();
        // Tariff
        services.AddScoped<INotificationHandler<TariffCreatedEvent>, TariffEventHandler>();
        services.AddScoped<INotificationHandler<TariffUpdatedEvent>, TariffEventHandler>();
        services.AddScoped<INotificationHandler<TariffDeletedEvent>, TariffEventHandler>();

        // Supply
        services.AddScoped<INotificationHandler<SupplyCreatedEvent>, SupplyEventHandler>();
        services.AddScoped<INotificationHandler<SupplyUpdatedEvent>, SupplyEventHandler>();
        services.AddScoped<INotificationHandler<SupplyDeletedEvent>, SupplyEventHandler>();
        // Meter
        services.AddScoped<INotificationHandler<MeterCreatedEvent>, MeterEventHandler>();
        services.AddScoped<INotificationHandler<MeterUpdatedEvent>, MeterEventHandler>();
        services.AddScoped<INotificationHandler<MeterDeletedEvent>, MeterEventHandler>();

        return services;
    }

    public static IServiceCollection AddApiUser(this IServiceCollection services)
    {
        // User
        services.AddScoped<IUser, ApiUser>();

        return services;
    }
}