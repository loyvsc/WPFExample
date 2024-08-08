using FluentValidation;
using WPFExample.Presentation.ViewModels;

namespace WPFExample.Presentation.Validators;

public class AuthValidator : AbstractValidator<AuthViewModel>
{
    public AuthValidator()
    {
        RuleFor(user => user.Login)
            .NotEmpty()
            .WithMessage("Please enter a login!");
        
        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Please enter a password!");
    }
}