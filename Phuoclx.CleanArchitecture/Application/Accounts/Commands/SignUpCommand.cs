namespace Application.Accounts.Commands
{
    public record SignUpCommand : IRequest<Result<ApplicationUser>> 
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public DateTime Dob { get; set; }
        public string? IdentityCardNumber { get; set; } = string.Empty;
    };

    public static class SignUpCommandExtension
    {
        public static ApplicationUser ToApplicationUser(this SignUpCommand request)
        {
            return new ApplicationUser()
            {
                UserName = request.Email,
                Email = request.Email,
                Gender = request.Gender,
                Dob = DateTime.ParseExact(request.Dob.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                IdentityCardNumber = string.IsNullOrEmpty(request.IdentityCardNumber) ? null : request.IdentityCardNumber,
            };
        }
    }

    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result<ApplicationUser>>
    {
        private readonly IIdentityService _identityService;

        public SignUpCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<ApplicationUser>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            // convert SignUpRequest to ApplicationUser
            var user = request.ToApplicationUser();

            // validation
            var validator = new SignUpCommandValidtor();
            var validationResult = await validator.ValidateAsync(request);
            if(!validationResult.IsValid)
            {
                // cause validation exception
                ValidationException exception = new (validationResult.Errors);
                return new Result<ApplicationUser>(exception);
            }

            var result = await _identityService.CreateAsync(user, 
                request.Password,
                Roles.Member);

            if(result is not null)
                return user;
            

            return null!;   
        }
    }
}
