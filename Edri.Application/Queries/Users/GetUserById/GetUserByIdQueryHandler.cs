using System.Threading;
using System.Threading.Tasks;
using Edri.Application.ViewModels.Users;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using MediatR;

namespace Edri.Application.Queries.Users.GetUserById;

public sealed class GetUserByIdQueryHandler :
    IRequestHandler<GetUserByIdQuery, UserViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMediatorHandler bus)
    {
        _userRepository = userRepository;
        _bus = bus;
    }

    public async Task<UserViewModel?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetUserByIdQuery),
                    $"User with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return UserViewModel.FromUser(user);
    }
}