namespace WashingMachineManagementApi.Infrastructure.Identity.Common.Models;

public class RefreshToken<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;

    public string Value { get; set; } = null!;

    public DateTime CreationDateTimeUtc { get; set; }

    public DateTime ExpirationDateTimeUtc { get; set; }

    public DateTime? RevokationDateTimeUtc { get; set; }

    public bool IsExpired => DateTime.UtcNow >= ExpirationDateTimeUtc;

    public bool IsActive => RevokationDateTimeUtc is null && !IsExpired;

    public TKey ApplicationUserId { get; set; } = default!;
}
