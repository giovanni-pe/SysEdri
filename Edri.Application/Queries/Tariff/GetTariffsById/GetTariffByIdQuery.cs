using System;
using Edri.Application.ViewModels.Tariffs;
using MediatR;

namespace Edri.Application.Queries.Tariffs.GetTariffById;

public sealed record GetTariffByIdQuery(Guid TariffId) : IRequest<TariffViewModel?>;