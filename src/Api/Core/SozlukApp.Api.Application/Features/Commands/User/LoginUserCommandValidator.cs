using FluentValidation;
using SozlukAppCommon.Models.RequestModels;

namespace SozlukApp.Api.Application.Features.Commands.User
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        // we don't need to inject this class anywhere  because we add FluentValidation in Registration class with assembly
        public LoginUserCommandValidator()
        {
            RuleFor(i =>  i.EmailAdress)
                .NotNull()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("{PropertyName} not a valid email!");

            RuleFor(i => i.Password)
                .NotNull()
                .MinimumLength(6)
                .WithMessage("{PropertyName} should at least be {MinLength} character..}");
        }
    }
}
