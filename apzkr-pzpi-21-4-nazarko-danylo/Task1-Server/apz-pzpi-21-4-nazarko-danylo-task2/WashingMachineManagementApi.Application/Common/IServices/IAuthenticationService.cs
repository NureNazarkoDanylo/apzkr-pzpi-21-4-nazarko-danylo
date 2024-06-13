using WashingMachineManagementApi.Application.Authentication;

namespace WashingMachineManagementApi.Application.Common.IServices;

public interface IAuthenticationService
{
    Task RegisterAsync(string email, string password, CancellationToken cancellationToken);

    Task<TokensModel> LoginAsync(string email, string password, CancellationToken cancellationToken);

    Task<TokensModel> RenewAccessTokenAsync(string refreshToken, CancellationToken cancellationToken);

    Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
}
