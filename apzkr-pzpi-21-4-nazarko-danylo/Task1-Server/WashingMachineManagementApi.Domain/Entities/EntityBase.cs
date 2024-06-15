namespace WashingMachineManagementApi.Domain.Entities;

public abstract class EntityBase<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default(TKey)!;
}
