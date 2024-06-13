namespace WashingMachineManagementApi.Application.Common.IServices;

public interface ISessionUserService
{
    public string? Id { get; }
    public string? Email { get; }
    public ICollection<string> Roles { get; }

    public bool IsAdministrator { get; }
    public bool IsAuthenticated { get; }
}
