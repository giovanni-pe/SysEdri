using System;

namespace Edri.Application.ViewModels.Departments;

public sealed record UpdateDepartmentViewModel(
    Guid Id,
    string Name,
    string Code
);