using FluentValidation;

namespace Service.Mediator.V1.AccountCase.Register
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(e => e.LastName)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(e => e.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
