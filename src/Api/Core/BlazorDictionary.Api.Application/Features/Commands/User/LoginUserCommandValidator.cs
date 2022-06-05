using BlazorDictionary.Common.Models.RequestModels;
using FluentValidation;

namespace BlazorDictionary.Api.Application.Features.Commands.User
{
    public class LoginUserCommandValidator:AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(i => i.EmailAddress)
                .NotNull()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("{PropertyName} not a valid email address");

            RuleFor(i => i.Password)
                .NotNull()
                .MinimumLength(6).WithMessage("{PropertyName} should at least be {MinLength} chracters");
        }
    }
}
