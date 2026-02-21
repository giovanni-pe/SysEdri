using System;

namespace Edri.Application.ViewModels.Provinces;

public sealed record UpdateProvinceViewModel(
    Guid Id,
    Guid depatmentId,
    string Name,
    string Code
);