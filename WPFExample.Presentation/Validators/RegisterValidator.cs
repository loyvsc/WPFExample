using FluentValidation;
using WPFExample.Presentation.ViewModels;

namespace WPFExample.Presentation.Validators;

public class RegisterValidator : AbstractValidator<RegisterViewModel>
{
    public RegisterValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Please enter a email!");

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Please enter a password!");
        
        RuleFor(user => user.FirstName)
            .NotEmpty()
            .WithMessage("Please enter a first name!");

        RuleFor(user => user.Lastname)
            .NotEmpty()
            .WithMessage("Please enter a last name!");
        
        RuleFor(user => user.Username)
            .NotEmpty()
            .WithMessage("Please enter a username!");
        
        RuleFor(user => user.PhoneNumber)
            .NotEmpty()
            .WithMessage("Please enter a phone number!");
        
    }
}