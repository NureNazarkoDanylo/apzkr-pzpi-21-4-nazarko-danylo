using System.IdentityModel.Tokens.Jwt;
using WashingMachineManagementApi.Application.Common.IServices;
using WashingMachineManagementApi.Application.Common.Models;

namespace WashingMachineManagementApi.Api.Services;

public class SessionUserService : ISessionUserService
{
    private readonly HttpContext _httpContext;

    public SessionUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext!;
    }

    public string? Id => _httpContext.User.Claims
        .FirstOrDefault(c => c.Properties
            .Any(p => p.Value == JwtRegisteredClaimNames.Sub))
        ?.Value;

    public string? Email => _httpContext.User.Claims
        .FirstOrDefault(c => c.Properties
            .Any(p => p.Value == JwtRegisteredClaimNames.Email))
        ?.Value;

    public ICollection<string> Roles => _httpContext.User.Claims
        .Where(c => c.Properties
            .Any(p => p.Value == "roles"))
        .Select(c => c.Value)
        .ToArray() ?? new string[0];

    public bool IsAdministrator => Roles.Contains(IdentityRoles.Administrator.ToString());

    public bool IsAuthenticated => Id != null;
}
