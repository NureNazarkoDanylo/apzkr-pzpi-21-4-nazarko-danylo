using AspNetCore.Identity.Mongo.Model;

namespace WashingMachineManagementApi.Infrastructure.Identity.MongoDb.Models;

public class ApplicationRole<TKey> : MongoRole<TKey>
    where TKey : IEquatable<TKey>
{
}
