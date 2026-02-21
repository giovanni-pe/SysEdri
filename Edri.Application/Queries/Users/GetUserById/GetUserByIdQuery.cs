using System;
using Edri.Application.ViewModels.Users;
using MediatR;

namespace Edri.Application.Queries.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserViewModel?>;