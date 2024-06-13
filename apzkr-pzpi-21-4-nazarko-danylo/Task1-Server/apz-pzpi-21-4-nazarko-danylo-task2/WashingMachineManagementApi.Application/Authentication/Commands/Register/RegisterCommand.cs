using MediatR;

namespace WashingMachineManagementApi.Application.Authentication.Commands.Register;

public record RegisterCommand : IRequest
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
