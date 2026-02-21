using System;
using Edri.Application.ViewModels.Supplies;
using MediatR;

namespace Edri.Application.Queries.Supplies.GetSupplyById;

public sealed record GetSupplyByIdQuery(Guid SupplyId) : IRequest<SupplyViewModel?>;