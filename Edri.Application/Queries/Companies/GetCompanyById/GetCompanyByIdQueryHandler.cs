using System.Threading;
using System.Threading.Tasks;
using Edri.Application.ViewModels.Companies;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using MediatR;

namespace Edri.Application.Queries.Companies.GetCompanyById;

public sealed class GetCompanyByIdQueryHandler :
    IRequestHandler<GetCompanyByIdQuery, CompanyViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ICompanyRepository _companyRepository;

    public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository, IMediatorHandler bus)
    {
        _companyRepository = companyRepository;
        _bus = bus;
    }

    public async Task<CompanyViewModel?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(request.CompanyId);

        if (company is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetCompanyByIdQuery),
                    $"Company with id {request.CompanyId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return CompanyViewModel.FromCompany(company);
    }
}