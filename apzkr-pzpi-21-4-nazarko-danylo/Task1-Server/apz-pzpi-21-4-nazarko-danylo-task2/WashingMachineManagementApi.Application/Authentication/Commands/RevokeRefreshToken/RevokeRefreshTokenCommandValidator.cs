using FluentValidation;

namespace WashingMachineManagementApi.Application.Authentication.Commands.RevokeRefreshToken;

public class RevokeRefreshTokenCommandValidator : AbstractValidator<RevokeRefreshTokenCommand>
{
    public RevokeRefreshTokenCommandValidator()
    {
        RuleFor(v => v.RefreshToken).NotEmpty();
    }
}
