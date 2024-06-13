using Microsoft.AspNetCore.Identity;

namespace WashingMachineManagementApi.Infrastructure.Identity.EntityFramework.Models;

public class ApplicationRole<TKey> : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{
}
