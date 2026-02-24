using System;
using Edri.Application.ViewModels.Readings;
using MediatR;

namespace Edri.Application.Queries.Readings.GetReadingById;

public sealed record GetReadingByIdQuery(Guid ReadingId) : IRequest<ReadingViewModel?>;