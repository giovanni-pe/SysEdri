using System;

namespace Edri.Application.ViewModels.Provinces;

public sealed record CreateProvinceViewModel(
    Guid depatmentId,
    string Name,
    string Code
);