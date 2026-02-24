using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Readings;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class ReadingViewModelSortProvider : ISortingExpressionProvider<ReadingViewModel, Reading>
{
    private static readonly Dictionary<string, Expression<Func<Reading, object>>> s_expressions = new()
    {
        { "id", reading => reading.Id },
        { "deviceId", reading => reading.DeviceId },
        { "meterNumber", reading => reading.MeterNumber },
        { "valueKwh", reading => reading.ValueKwh },
        { "readingDate", reading => reading.ReadingDate },
        { "source", reading => reading.Source },
        { "ocrConfidence", reading => reading.OcrConfidence! }
    };

    public Dictionary<string, Expression<Func<Reading, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}