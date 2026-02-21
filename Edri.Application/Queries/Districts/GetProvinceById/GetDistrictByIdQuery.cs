using System;
using Edri.Application.ViewModels.Districts;
using MediatR;

namespace Edri.Application.Queries.Districts.GetDistrictById;

public sealed record GetDistrictByIdQuery(Guid DistrictId) : IRequest<DistrictViewModel?>;