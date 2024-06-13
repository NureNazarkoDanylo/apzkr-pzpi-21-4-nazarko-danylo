using WashingMachineManagementApi.Infrastructure.Identity.Common.Models;

namespace WashingMachineManagementApi.Infrastructure.Identity.Common.Interfaces;

public interface IApplicationUser<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public ICollection<RefreshToken<TKey>> RefreshTokens { get; set; }
}
