using System;
using Edri.Domain.Enums;

namespace Edri.Application.ViewModels.Users;

public sealed record UpdateUserViewModel(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    UserRole Role,
    Guid TenantId);