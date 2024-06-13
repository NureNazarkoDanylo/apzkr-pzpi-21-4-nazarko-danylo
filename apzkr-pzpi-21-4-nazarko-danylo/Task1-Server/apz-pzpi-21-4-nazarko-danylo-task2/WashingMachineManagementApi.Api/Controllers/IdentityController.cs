using WashingMachineManagementApi.Application.Authentication.Commands.Register;
using WashingMachineManagementApi.Application.Authentication.Commands.RenewAccessToken;
using WashingMachineManagementApi.Application.Authentication.Commands.RevokeRefreshToken;
using WashingMachineManagementApi.Application.Authentication.Queries.Login;
using Microsoft.AspNetCore.Mvc;
using WashingMachineManagementApi.Application.Authentication;

namespace WashingMachineManagementApi.Api.Controllers;

[Route("identity")]
public class IdentityController : BaseController
{
    [HttpPost("register")]
    public async Task Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
    }
    
    [HttpPost("login")]
    public async Task<TokensModel> Login([FromBody] LoginQuery query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpPost("renewAccessToken")]
    public async Task<TokensModel> RenewAccessToken([FromBody] RenewAccessTokenCommand command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpPost("revokeRefreshToken")]
    public async Task RevokeRefreshToken([FromBody] RevokeRefreshTokenCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
    }
}
