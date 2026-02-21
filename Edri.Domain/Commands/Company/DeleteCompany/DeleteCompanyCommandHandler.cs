using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Company;
using MediatR;

namespace Edri.Domain.Commands.Companies.DeleteCompany;

public sealed class DeleteCompanyCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteCompanyCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICompanyRepository companyRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _companyRepository = companyRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to delete Company {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));
            return;
        }

        var company = await _companyRepository.GetByIdAsync(request.AggregateId);

        if (company is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Company with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }

        _companyRepository.Remove(company);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CompanyDeletedEvent(company.Id));
        }
    }
}