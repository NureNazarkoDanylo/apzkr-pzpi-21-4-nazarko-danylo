using MediatR;

namespace WashingMachineManagementApi.Application.Authentication.Queries.Login;

public record LoginQuery : IRequest<TokensModel>
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
