using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Captures;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class CaptureViewModelSortProvider : ISortingExpressionProvider<CaptureViewModel, Capture>
{
    private static readonly Dictionary<string, Expression<Func<Capture, object>>> s_expressions = new()
    {
        { "id", capture => capture.Id },
        { "deviceId", capture => capture.DeviceId },
        { "timestamp", capture => capture.Timestamp },
        { "status", capture => capture.Status }
    };

    public Dictionary<string, Expression<Func<Capture, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}