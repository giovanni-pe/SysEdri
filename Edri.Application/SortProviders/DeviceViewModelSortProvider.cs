using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Devices;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class DeviceViewModelSortProvider : ISortingExpressionProvider<DeviceViewModel, Device>
{
    private static readonly Dictionary<string, Expression<Func<Device, object>>> s_expressions = new()
    {
        { "id", device => device.Id },
        { "code", device => device.Code },
        { "meterId", device => device.MeterId },
        { "branchId", device => device.BranchId },
        { "model", device => device.Model! },
        { "status", device => device.Status },
        { "lastConnection", device => device.LastConnection! },
        { "installationDate", device => device.InstallationDate }
    };

    public Dictionary<string, Expression<Func<Device, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}