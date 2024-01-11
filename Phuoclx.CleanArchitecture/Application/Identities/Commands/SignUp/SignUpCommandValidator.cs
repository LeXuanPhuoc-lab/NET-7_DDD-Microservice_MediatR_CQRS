namespace Application.Identity.Commands.SignUp
{
    public class SignUpCommandValidtor : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidtor()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.IdentityCardNumber)
                .Length(12);
            //.WithMessage("Identity card maximum length is 12");
            //RuleFor(x => x.SignUpRequest.Password)
            //    .Matches(@"")
            //    .WithMessage("");
        }
    }
}
