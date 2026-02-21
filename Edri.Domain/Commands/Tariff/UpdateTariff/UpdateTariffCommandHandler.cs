using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Tariff;
using MediatR;

namespace Edri.Domain.Commands.Tariffs.UpdateTariff;

public sealed class UpdateTariffCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateTariffCommand>
{
    private readonly ITariffRepository _tariffRepository;
    private readonly IUser _user;

    public UpdateTariffCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ITariffRepository tariffRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _tariffRepository = tariffRepository;
        _user = user;
    }

    public async Task Handle(UpdateTariffCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to update Tariff {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        var tariff = await _tariffRepository.GetByIdAsync(request.AggregateId);

        if (tariff is null)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is no Tariff with Id {request.AggregateId}",
                "OBJECT_NOT_FOUND"));
            return;
        }

        tariff.Update(
            request.CompanyId,
            request.Code,
            request.Name,
            request.PricePerKwh,
            request.FixedCharge,
            request.Description,
            request.EffectiveFrom,
            request.EffectiveTo,
            request.IsActive);

        _tariffRepository.Update(tariff);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new TariffUpdatedEvent(
                tariff.Id,
                tariff.CompanyId,
                tariff.Code,
                tariff.Name));
        }
    }
}