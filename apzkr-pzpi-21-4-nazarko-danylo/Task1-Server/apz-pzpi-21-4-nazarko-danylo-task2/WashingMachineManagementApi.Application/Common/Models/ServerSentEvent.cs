namespace WashingMachineManagementApi.Application.Common.Models;

public class ServerSentEvent
{
    public string? Event { get; set; } = String.Empty;

    public string Data { get; set; } = String.Empty;
}
