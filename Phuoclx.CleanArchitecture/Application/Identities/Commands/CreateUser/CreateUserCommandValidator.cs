namespace Application.Identity.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Username)
                .EmailAddress()
                .NotEmpty()
                .WithMessage("Wrong email format");

            RuleFor(x => x.Password)
                .Matches(@"")
                .NotEmpty()
                .WithMessage("Wrong password format");

            RuleFor(x => x.IdentityCardNumber)
                .Length(12)
                .WithMessage("Identity Card Number max length is 12 char");
        }
    }
}
