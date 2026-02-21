using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Company;
using MediatR;

namespace Edri.Domain.Commands.Companies.UpdateCompany;

public sealed class UpdateCompanyCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUser _user;

    public UpdateCompanyCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICompanyRepository companyRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _companyRepository = companyRepository;
        _user = user;
    }

    public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to update Company {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        var company = await _companyRepository.GetByIdAsync(request.AggregateId);

        if (company is null)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is no Company with Id {request.AggregateId}",
                "OBJECT_NOT_FOUND"));
            return;
        }

        company.Update(
            request.TaxId,
            request.BusinessName,
            request.TradeName,
            request.FiscalAddress,
            request.Phone,
            request.Email,
            request.LogoUrl);

        _companyRepository.Update(company);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CompanyUpdatedEvent(
                company.Id,
                company.TaxId,
                company.BusinessName));
        }
    }
}