using System;
using Edri.Application.ViewModels.Devices;
using MediatR;

namespace Edri.Application.Queries.Devices.GetDeviceById;

public sealed record GetDeviceByIdQuery(Guid DeviceId) : IRequest<DeviceViewModel?>;