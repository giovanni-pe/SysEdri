using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Customer;
using MediatR;

namespace Edri.Domain.Commands.Customers.DeleteCustomer;

public sealed class DeleteCustomerCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteCustomerCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICustomerRepository customerRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete Customer {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));
            return;
        }

        var customer = await _customerRepository.GetByIdAsync(request.AggregateId);

        if (customer is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Customer with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }

        _customerRepository.Remove(customer);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CustomerDeletedEvent(customer.Id));
        }
    }
}