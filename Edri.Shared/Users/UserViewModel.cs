using System;

namespace Edri.Shared.Users;

public sealed record UserViewModel(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    DateTimeOffset? DeletedAt);