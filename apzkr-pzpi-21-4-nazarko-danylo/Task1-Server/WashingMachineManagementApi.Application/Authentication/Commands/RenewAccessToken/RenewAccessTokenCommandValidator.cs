using FluentValidation;

namespace WashingMachineManagementApi.Application.Authentication.Commands.RenewAccessToken;

public class RenewAccessTokenCommandValidator : AbstractValidator<RenewAccessTokenCommand>
{
    public RenewAccessTokenCommandValidator()
    {
        RuleFor(v => v.RefreshToken).NotEmpty();
    }
}
