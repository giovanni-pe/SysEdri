using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Customer;
using MediatR;

namespace Edri.Domain.Commands.Customers.UpdateCustomer;

public sealed class UpdateCustomerCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUser _user;

    public UpdateCustomerCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICustomerRepository customerRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _customerRepository = customerRepository;
        _user = user;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        // 1. Validaciones de formato (FluentValidation)
        if (!await TestValidityAsync(request))
        {
            return;
        }

        // 2. Validación de Permisos (Solo Admin)
        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to update Customer {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        // 3. Obtener la entidad
        var customer = await _customerRepository.GetByIdAsync(request.AggregateId);

        if (customer is null)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is no Customer with Id {request.AggregateId}",
                "OBJECT_NOT_FOUND"));
            return;
        }

        // 4. Actualizar Entidad
        customer.Update(
            request.UserId,
            request.DistrictId,
            request.DocumentType,
            request.DocumentNumber,
            request.CustomerType,
            request.BusinessName,
            request.FirstName,
            request.LastName,
            request.Address,
            request.Phone,
            request.Email,
            request.Status);

        _customerRepository.Update(customer);

        // 5. Persistir y lanzar evento
        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CustomerUpdatedEvent(
                customer.Id,
                customer.UserId,
                customer.DistrictId,
                customer.DocumentType,
                customer.DocumentNumber,
                customer.CustomerType,
                customer.BusinessName,
                customer.FirstName,
                customer.LastName,
                customer.Status));
        }
    }
}