using System;
using Edri.Application.ViewModels.Meters;
using MediatR;

namespace Edri.Application.Queries.Meters.GetMeterById;

public sealed record GetMeterByIdQuery(Guid MeterId) : IRequest<MeterViewModel?>;