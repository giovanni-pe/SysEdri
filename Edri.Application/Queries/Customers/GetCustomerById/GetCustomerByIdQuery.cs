using System;
using Edri.Application.ViewModels.Customers;
using MediatR;

namespace Edri.Application.Queries.Customers.GetCustomerById;

public sealed record GetCustomerByIdQuery(Guid CustomerId) : IRequest<CustomerViewModel?>;