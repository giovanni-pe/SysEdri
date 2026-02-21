using System;

namespace Edri.Application.ViewModels.Users;

public sealed record CreateUserViewModel(
    string Email,
    string FirstName,
    string LastName,
    string Password,
    Guid TenantId);