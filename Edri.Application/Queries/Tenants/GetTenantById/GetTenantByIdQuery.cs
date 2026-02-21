using System;
using Edri.Application.ViewModels.Tenants;
using MediatR;

namespace Edri.Application.Queries.Tenants.GetTenantById;

public sealed record GetTenantByIdQuery(Guid TenantId) : IRequest<TenantViewModel?>;