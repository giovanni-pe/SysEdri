using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Tariff;
using MediatR;

namespace Edri.Domain.Commands.Tariffs.CreateTariff;

public sealed class CreateTariffCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateTariffCommand>
{
    private readonly ITariffRepository _tariffRepository;
    private readonly IUser _user;

    public CreateTariffCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ITariffRepository tariffRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _tariffRepository = tariffRepository;
        _user = user;
    }

    public async Task Handle(CreateTariffCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to create Tariff {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        if (await _tariffRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is already a Tariff with Id {request.AggregateId}",
                "TARIFF_ALREADY_EXISTS"));
            return;
        }

        var tariff = new Tariff(
            request.AggregateId,
            request.CompanyId,
            request.Code,
            request.Name,
            request.PricePerKwh,
            request.FixedCharge,
            request.Description,
            request.EffectiveFrom,
            request.EffectiveTo,
            request.IsActive);

        _tariffRepository.Add(tariff);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new TariffCreatedEvent(
                tariff.Id,
                tariff.CompanyId,
                tariff.Code,
                tariff.Name));
        }
    }
}