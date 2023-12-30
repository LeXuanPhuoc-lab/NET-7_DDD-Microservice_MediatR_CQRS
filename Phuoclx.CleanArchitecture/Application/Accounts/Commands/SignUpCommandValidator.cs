namespace Application.Accounts.Commands
{
    public class SignUpCommandValidtor : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidtor()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.IdentityCardNumber)
                .MaximumLength(12);
            //RuleFor(x => x.SignUpRequest.Password)
            //    .Matches(@"")
            //    .WithMessage("");
        }
    }
}
