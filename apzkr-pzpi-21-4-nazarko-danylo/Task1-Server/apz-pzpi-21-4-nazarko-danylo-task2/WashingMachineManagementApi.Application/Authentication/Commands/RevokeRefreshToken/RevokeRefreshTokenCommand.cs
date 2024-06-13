using MediatR;

namespace WashingMachineManagementApi.Application.Authentication.Commands.RevokeRefreshToken;

public record RevokeRefreshTokenCommand : IRequest 
{
    public required string RefreshToken { get; set; }
}
