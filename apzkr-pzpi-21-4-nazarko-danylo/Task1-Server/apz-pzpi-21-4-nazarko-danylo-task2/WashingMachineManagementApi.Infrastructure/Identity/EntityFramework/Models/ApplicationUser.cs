using Microsoft.AspNetCore.Identity;
using WashingMachineManagementApi.Infrastructure.Identity.Common.Interfaces;
using WashingMachineManagementApi.Infrastructure.Identity.Common.Models;

namespace WashingMachineManagementApi.Infrastructure.Identity.EntityFramework.Models;

public class ApplicationUser<TKey> : IdentityUser<TKey>, IApplicationUser<TKey>
    where TKey : IEquatable<TKey>
{
    public ICollection<RefreshToken<TKey>> RefreshTokens { get; set; } = default!;
}
