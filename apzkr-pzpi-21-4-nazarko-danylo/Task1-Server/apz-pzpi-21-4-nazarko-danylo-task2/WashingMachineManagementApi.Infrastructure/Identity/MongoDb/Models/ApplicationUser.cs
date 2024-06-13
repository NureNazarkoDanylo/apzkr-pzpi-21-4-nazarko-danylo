using AspNetCore.Identity.Mongo.Model;
using WashingMachineManagementApi.Infrastructure.Identity.Common.Interfaces;
using WashingMachineManagementApi.Infrastructure.Identity.Common.Models;

namespace WashingMachineManagementApi.Infrastructure.Identity.MongoDb.Models;

public class ApplicationUser<TKey> : MongoUser<TKey>, IApplicationUser<TKey>
    where TKey : IEquatable<TKey>
{
    public ICollection<RefreshToken<TKey>> RefreshTokens { get; set; } = default!;
}
