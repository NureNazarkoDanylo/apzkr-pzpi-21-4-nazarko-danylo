using WashingMachineManagementApi.Application.Common.IServices;
using MediatR;

namespace WashingMachineManagementApi.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IAuthenticationService _authenticationService;

    public RegisterCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _authenticationService.RegisterAsync(request.Email, request.Password, cancellationToken);
    }
}
