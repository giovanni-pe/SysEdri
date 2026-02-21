using Edri.Application.Interfaces;
using Edri.Application.Queries.Departments.GetDepartmentById;
using Edri.Application.Queries.Provinces.GetProvinceById;
using Edri.Application.Queries.Districts.GetDistrictById;
using Edri.Application.Queries.Customers.GetCustomerById;
using Edri.Application.Queries.Tenants.GetAll;
using Edri.Application.Queries.Tenants.GetTenantById;
using Edri.Application.Queries.Users.GetAll;
using Edri.Application.Queries.Users.GetUserById;
using Edri.Application.Services;
using Edri.Application.SortProviders;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Tenants;
using Edri.Application.ViewModels.Users;
using Edri.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Edri.Application.ViewModels.Departments;
using Edri.Application.ViewModels.Provinces;
using Edri.Application.ViewModels.Districts;
using Edri.Application.ViewModels.Customers;
using Edri.Application.Queries.Departments.GetAll;
using Edri.Application.Queries.Provinces.GetAll;
using Edri.Application.Queries.Districts.GetAll;
using Edri.Application.Queries.Customers.GetAll;
using Edri.Application.Queries.Companies.GetCompanyById;
using Edri.Application.Queries.Companies.GetAll;
using Edri.Application.ViewModels.Companies;
using Edri.Application.Queries.Branches.GetBranchById;
using Edri.Application.Queries.Branches.GetAll;
using Edri.Application.ViewModels.Branches;
using Edri.Application.Queries.Tariffs.GetTariffById;
using Edri.Application.Queries.Tariffs.GetAll;
using Edri.Application.ViewModels.Tariffs;
using Edri.Application.Queries.Supplies.GetSupplyById;
using Edri.Application.Queries.Supplies.GetAll;
using Edri.Application.ViewModels.Supplies;
using Edri.Application.Queries.Meters.GetMeterById;
using Edri.Application.Queries.Meters.GetAll;
using Edri.Application.ViewModels.Meters;

namespace Edri.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IProvinceService, ProvinceService>();
        services.AddScoped<IDistrictService, DistrictService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<ITariffService, TariffService>();
        services.AddScoped<ISupplyService, SupplyService>();
        services.AddScoped<IMeterService, MeterService>();

        return services;
    }

    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        // User
        services.AddScoped<IRequestHandler<GetUserByIdQuery, UserViewModel?>, GetUserByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllUsersQuery, PagedResult<UserViewModel>>, GetAllUsersQueryHandler>();

        // Tenant
        services.AddScoped<IRequestHandler<GetTenantByIdQuery, TenantViewModel?>, GetTenantByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<GetAllTenantsQuery, PagedResult<TenantViewModel>>, GetAllTenantsQueryHandler>();

        // Department
        services.AddScoped<IRequestHandler<GetDepartmentByIdQuery, DepartmentViewModel?>, GetDepartmentByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<GetAllDepartmentsQuery, PagedResult<DepartmentViewModel>>, GetAllDepartmentsQueryHandler>();

        // Province
        services.AddScoped<IRequestHandler<GetProvinceByIdQuery, ProvinceViewModel?>, GetProvinceByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<GetAllProvincesQuery, PagedResult<ProvinceViewModel>>, GetAllProvincesQueryHandler>();

        // District
        services.AddScoped<IRequestHandler<GetDistrictByIdQuery, DistrictViewModel?>, GetDistrictByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<GetAllDistrictsQuery, PagedResult<DistrictViewModel>>, GetAllDistrictsQueryHandler>();

        // Customer
        services.AddScoped<IRequestHandler<GetCustomerByIdQuery, CustomerViewModel?>, GetCustomerByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<GetAllCustomersQuery, PagedResult<CustomerViewModel>>, GetAllCustomersQueryHandler>();
        // Company

        services.AddScoped<IRequestHandler<GetCompanyByIdQuery, CompanyViewModel?>, GetCompanyByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllCompaniesQuery, PagedResult<CompanyViewModel>>, GetAllCompaniesQueryHandler>();
        // Branch
        services.AddScoped<IRequestHandler<GetBranchByIdQuery, BranchViewModel?>, GetBranchByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllBranchesQuery, PagedResult<BranchViewModel>>, GetAllBranchesQueryHandler>();
        // Tariff
        services.AddScoped<IRequestHandler<GetTariffByIdQuery, TariffViewModel?>, GetTariffByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllTariffsQuery, PagedResult<TariffViewModel>>, GetAllTariffsQueryHandler>();
        // Supply
        services.AddScoped<IRequestHandler<GetSupplyByIdQuery, SupplyViewModel?>, GetSupplyByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllSuppliesQuery, PagedResult<SupplyViewModel>>, GetAllSuppliesQueryHandler>();
        // Meter
        services.AddScoped<IRequestHandler<GetMeterByIdQuery, MeterViewModel?>, GetMeterByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllMetersQuery, PagedResult<MeterViewModel>>, GetAllMetersQueryHandler>();
        return services;
    }

    public static IServiceCollection AddSortProviders(this IServiceCollection services)
    {
        services.AddScoped<ISortingExpressionProvider<TenantViewModel, Tenant>, TenantViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<UserViewModel, User>, UserViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<DepartmentViewModel, Department>, DepartmentViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<ProvinceViewModel, Province>, ProvinceViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<DistrictViewModel, District>, DistrictViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<CustomerViewModel, Customer>, CustomerViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<CompanyViewModel, Company>, CompanyViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<BranchViewModel, Branch>, BranchViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<TariffViewModel, Tariff>, TariffViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<SupplyViewModel, Supply>, SupplyViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<MeterViewModel, Meter>, MeterViewModelSortProvider>();

        return services;
    }
}