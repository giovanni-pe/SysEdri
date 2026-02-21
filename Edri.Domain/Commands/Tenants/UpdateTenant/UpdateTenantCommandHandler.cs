using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Tenant;
using MediatR;

namespace Edri.Domain.Commands.Tenants.UpdateTenant;

public sealed class UpdateTenantCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateTenantCommand>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IUser _user;

    public UpdateTenantCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ITenantRepository tenantRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _tenantRepository = tenantRepository;
        _user = user;
    }

    public async Task Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
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
                    $"No permission to update tenant {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        var tenant = await _tenantRepository.GetByIdAsync(request.AggregateId);

        if (tenant is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no tenant with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        tenant.SetName(request.Name);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new TenantUpdatedEvent(
                tenant.Id,
                tenant.Name));
        }
    }
}