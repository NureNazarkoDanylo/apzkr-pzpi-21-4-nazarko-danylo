using FluentValidation;

namespace WashingMachineManagementApi.Application.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Email address is required.");
            // .EmailAddress().WithMessage("Email address is invalid.");

        RuleFor(v => v.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
