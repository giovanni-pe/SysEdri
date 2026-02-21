using System.Threading;
using System.Threading.Tasks;
using Edri.Application.ViewModels.Customers;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using MediatR;

namespace Edri.Application.Queries.Customers.GetCustomerById;

public sealed class GetCustomerByIdQueryHandler :
    IRequestHandler<GetCustomerByIdQuery, CustomerViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMediatorHandler bus)
    {
        _customerRepository = customerRepository;
        _bus = bus;
    }

    public async Task<CustomerViewModel?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetCustomerByIdQuery),
                    $"Customer with id {request.CustomerId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return CustomerViewModel.FromCustomer(customer);
    }
}