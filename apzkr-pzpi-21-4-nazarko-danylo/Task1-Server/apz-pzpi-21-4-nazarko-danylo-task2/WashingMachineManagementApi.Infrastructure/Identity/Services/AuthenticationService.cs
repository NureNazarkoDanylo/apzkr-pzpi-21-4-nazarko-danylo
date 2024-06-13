using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WashingMachineManagementApi.Application.Common.Exceptions;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;
using WashingMachineManagementApi.Application.Authentication;
using WashingMachineManagementApi.Infrastructure.Identity.MongoDb.Models;
using WashingMachineManagementApi.Infrastructure.Identity.Common.Models;

namespace WashingMachineManagementApi.Infrastructure.Identity.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser<string>> _userManager;
    private readonly RoleManager<ApplicationRole<string>> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthenticationService(
        UserManager<ApplicationUser<string>> userManager,
        RoleManager<ApplicationRole<string>> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _userManager.UserValidators.Clear();
        _userManager.PasswordValidators.Clear();

        _roleManager = roleManager;

        _configuration = configuration;
    }

    public async Task RegisterAsync(string email, string password, CancellationToken cancellationToken)
    {
        var userWithSameEmail = await _userManager.FindByEmailAsync(email);
        if (userWithSameEmail is not null)
        {
            throw new RegistrationException("User with given email already registered.");
        }

        var roles = _roleManager.Roles
            .Where(r => r.Name == IdentityRoles.User.ToString())
            .Select(r => r.Id)
            .ToList();

        var newUser = new ApplicationUser<string>
        {
            Id = Guid.NewGuid().ToString(),
            Email = email,
            Roles = roles,
            RefreshTokens = default!
        };

        var createUserResult = await _userManager.CreateAsync(newUser, password);

        if (createUserResult.Errors.Any())
        {
            throw new Exception();
        }
    }

    public async Task<TokensModel> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            throw new LoginException("No users registered with given email.");
        }

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordCorrect)
        {
            throw new LoginException("Given password is incorrect.");
        }

        var jwtSecurityToken = await CreateJwtAsync(user, cancellationToken);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        var refreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
        if (refreshToken is null)
        {
            refreshToken = CreateRefreshToken();
            refreshToken.ApplicationUserId = user.Id;
            user.RefreshTokens.Add(refreshToken);
            var result = await _userManager.UpdateAsync(user);
        }

        return new TokensModel(accessToken, refreshToken.Value);
    }

    public async Task<TokensModel> RenewAccessTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(rt => rt.Value == refreshToken));
        if (user is null)
        {
            throw new RenewAccessTokenException($"Refresh token {refreshToken} was not found.");
        }

        var refreshTokenObject = user.RefreshTokens.Single(rt => rt.Value == refreshToken);
        if (!refreshTokenObject.IsActive)
        {
            throw new RenewAccessTokenException("Refresh token is inactive.");
        }

        var jwtSecurityToken = await CreateJwtAsync(user, cancellationToken);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return new TokensModel(accessToken, refreshToken);
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Value == refreshToken));
        if (user is null)
        {
            throw new RevokeRefreshTokenException("Invalid refreshToken");
        }

        var refreshTokenObject = user.RefreshTokens.Single(x => x.Value == refreshToken);
        if (!refreshTokenObject.IsActive)
        {
            throw new RevokeRefreshTokenException("RefreshToken already revoked");
        }

        refreshTokenObject.RevokationDateTimeUtc = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);
    }

    private async Task<JwtSecurityToken> CreateJwtAsync(ApplicationUser<string> user, CancellationToken cancellationToken)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);

        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role));
        }

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var validity = TimeSpan.Parse(_configuration["Infrastructure:Authentication:JsonWebToken:AccessTokenValidity"]);
        var expirationDateTimeUtc = DateTime.UtcNow.Add(validity);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Infrastructure:Authentication:JsonWebToken:IssuerSigningKey"]));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration["Infrastructure:Authentication:JsonWebToken:Issuer"],
            audience: _configuration["Infrastructure:Authentication:JsonWebToken:Audience"],
            claims: claims,
            expires: expirationDateTimeUtc,
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }

    private RefreshToken<string> CreateRefreshToken()
    {
        var randomNumber = new byte[32];

        using var rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(randomNumber);

        var validity = TimeSpan.Parse(_configuration["Infrastructure:Authentication:JsonWebToken:RefreshTokenValidity"]);

        return new RefreshToken<string>
        {
            Id = Guid.NewGuid().ToString(),
            Value = Convert.ToBase64String(randomNumber),
            CreationDateTimeUtc = DateTime.UtcNow,
            ExpirationDateTimeUtc = DateTime.UtcNow.Add(validity)
        };
    }
}
