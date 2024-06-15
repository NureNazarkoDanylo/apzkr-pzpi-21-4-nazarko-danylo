using FluentValidation;

namespace WashingMachineManagementApi.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        // https://regexr.com/2ri2c
        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .Matches(@"\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b").WithMessage("Email address is invalid.");

        RuleFor(v => v.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(64).WithMessage("Password must be at most 64 characters long.")
            .Matches(@"(?=.*[A-Z]).*").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"(?=.*[a-z]).*").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"(?=.*[\d]).*").WithMessage("Password must contain at least one digit.")
            .Matches(@"(?=.*[!@#$%^&*()]).*").WithMessage("Password must contain at least one of the following special charactters: !@#$%^&*().");
    }
}
