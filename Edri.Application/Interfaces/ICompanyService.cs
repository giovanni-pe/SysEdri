using System;
using System.Threading.Tasks;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Companies;

namespace Edri.Application.Interfaces;

public interface ICompanyService
{
    Task<Guid> CreateCompanyAsync(CreateCompanyViewModel company);
    Task UpdateCompanyAsync(UpdateCompanyViewModel company);
    Task DeleteCompanyAsync(Guid companyId);
    Task<CompanyViewModel?> GetCompanyByIdAsync(Guid companyId);
    Task<PagedResult<CompanyViewModel>> GetAllCompaniesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}