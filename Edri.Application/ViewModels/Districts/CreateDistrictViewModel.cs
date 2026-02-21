using System;

namespace Edri.Application.ViewModels.Districts;

public sealed record CreateDistrictViewModel(
    Guid provinceId,
    string Name,
    string Code
);