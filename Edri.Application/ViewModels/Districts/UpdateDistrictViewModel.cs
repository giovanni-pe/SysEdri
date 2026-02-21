using System;

namespace Edri.Application.ViewModels.Districts;

public sealed record UpdateDistrictViewModel(
    Guid Id,
    Guid provinceId,
    string Name,
    string Code
);