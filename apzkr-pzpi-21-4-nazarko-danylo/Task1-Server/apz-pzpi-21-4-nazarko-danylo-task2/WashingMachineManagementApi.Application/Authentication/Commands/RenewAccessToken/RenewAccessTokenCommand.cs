using MediatR;

namespace WashingMachineManagementApi.Application.Authentication.Commands.RenewAccessToken;

public record RenewAccessTokenCommand : IRequest<TokensModel>
{
    public required string RefreshToken { get; set; }
}
