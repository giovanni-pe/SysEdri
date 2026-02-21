using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Company;
using MediatR;

namespace Edri.Domain.Commands.Companies.CreateCompany;

public sealed class CreateCompanyCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUser _user;

    public CreateCompanyCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICompanyRepository companyRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _companyRepository = companyRepository;
        _user = user;
    }

    public async Task Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to create Company {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        if (await _companyRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is already a Company with Id {request.AggregateId}",
                "COMPANY_ALREADY_EXISTS"));
            return;
        }

        var company = new Company(
            request.AggregateId,
            request.TaxId,
            request.BusinessName,
            request.TradeName,
            request.FiscalAddress,
            request.Phone,
            request.Email,
            request.LogoUrl);

        _companyRepository.Add(company);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CompanyCreatedEvent(
                company.Id,
                company.TaxId,
                company.BusinessName));
        }
    }
}