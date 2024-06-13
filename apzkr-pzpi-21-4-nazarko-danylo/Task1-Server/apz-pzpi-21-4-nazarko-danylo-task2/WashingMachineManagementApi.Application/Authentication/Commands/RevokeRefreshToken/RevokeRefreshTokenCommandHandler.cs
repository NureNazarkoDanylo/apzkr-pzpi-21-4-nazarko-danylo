using WashingMachineManagementApi.Application.Common.IServices;
using MediatR;

namespace WashingMachineManagementApi.Application.Authentication.Commands.RevokeRefreshToken;

public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand>
{
    private readonly IAuthenticationService _authenticationService;

    public RevokeRefreshTokenCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        await _authenticationService.RevokeRefreshTokenAsync(request.RefreshToken, cancellationToken);
    }
}
