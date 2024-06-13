using WashingMachineManagementApi.Application.Common.IServices;
using MediatR;

namespace WashingMachineManagementApi.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, TokensModel>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginQueryHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<TokensModel> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        return await _authenticationService.LoginAsync(request.Email, request.Password, cancellationToken);
    }
}
