using System;
using Edri.Application.ViewModels.Captures;
using MediatR;

namespace Edri.Application.Queries.Captures.GetCaptureById;

public sealed record GetCaptureByIdQuery(Guid CaptureId) : IRequest<CaptureViewModel?>;