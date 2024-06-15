namespace WashingMachineManagementApi.Application.Authentication;

public class TokensModel
{
    public TokensModel(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public string AccessToken { get; protected set; }

    public string RefreshToken { get; protected set; }
}
