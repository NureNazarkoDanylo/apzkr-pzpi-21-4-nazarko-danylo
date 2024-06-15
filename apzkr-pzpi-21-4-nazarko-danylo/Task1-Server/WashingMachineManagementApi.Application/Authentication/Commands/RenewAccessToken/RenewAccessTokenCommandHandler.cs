using WashingMachineManagementApi.Application.Common.IServices;
using MediatR;

namespace WashingMachineManagementApi.Application.Authentication.Commands.RenewAccessToken;

public class RenewAccessTokenCommandHandler : IRequestHandler<RenewAccessTokenCommand, TokensModel>
{
    private readonly IAuthenticationService _authenticationService;

    public RenewAccessTokenCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<TokensModel> Handle(RenewAccessTokenCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.RenewAccessTokenAsync(request.RefreshToken, cancellationToken);
    }
}
