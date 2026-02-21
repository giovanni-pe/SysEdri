using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Customer;
using MediatR;

namespace Edri.Domain.Commands.Customers.CreateCustomer;

public sealed class CreateCustomerCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUser _user;

    public CreateCustomerCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICustomerRepository customerRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _customerRepository = customerRepository;
        _user = user;
    }

    public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to create Customer {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        if (await _customerRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is already a Customer with Id {request.AggregateId}",
                "CUSTOMER_ALREADY_EXISTS"));
            return;
        }

        var customer = new Customer(
            request.AggregateId,
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

        _customerRepository.Add(customer);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CustomerCreatedEvent(
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