namespace Application.Accounts.Queries
{
    public record SignInQuery(string Username, string Password) : IRequest<Result<AuthenticationResult>>;

    public class SignInQueryHandler : IRequestHandler<SignInQuery, Result<AuthenticationResult>>
    {
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly IIdentityRepository _identityRepo;
        private readonly IRefreshTokenRepository _refreshTokenRepo;

        //private readonly IIdentityService _identityService;

        private readonly AppSettings _appSettings;

        public SignInQueryHandler(SignInManager<ApplicationUser> signInManger,
            RoleManager<IdentityRole> roleManager,
            //IIdentityService identityService,
            IIdentityRepository identityRepo,
            IRefreshTokenRepository refreshTokenRepo,
            IOptionsMonitor<AppSettings> monitor) 
        {
            _signInManger = signInManger;
            //_identityService = identityService;
            _identityRepo = identityRepo;
            _refreshTokenRepo = refreshTokenRepo;
            _appSettings = monitor.CurrentValue;
        }

        public async Task<Result<AuthenticationResult>> Handle(SignInQuery request, 
            CancellationToken cancellationToken)
        {
            //// validation
            //var validator = new SignInQueryValidator();
            //var validateResult = await validator.ValidateAsync(request);
            //if (!validateResult.IsValid)
            //{
            //    ValidationException exception = new(validateResult.Errors);
            //    return new Result<AuthenticationResult>(exception);
            //}

            var user = await _identityRepo.GetUserNameAsync(request.Username);
            if (user != null) 
            {
                // check password 
                var result = new CustomPasswordHasher().VerifyHashedPassword(user,
                    user.PasswordHash!, request.Password);

                if(result == PasswordVerificationResult.Failed) return null!;

                var roles = await _identityRepo.GetRolesbyUserIdAsync(user.Id);

                //await _signInManger.SignInAsync(user, isPersistent: false);

                // Generate token and refresh token
                var generateResult =  new JwtHelper(_appSettings).GenerateTokenWithTokenId(user,
                    roles != null ? roles.ToArray() : null!);
                var refreshToken = await _refreshTokenRepo.GenerateRefreshTokenForUserAsync(user, generateResult.TokenId);

                return new AuthenticationResult()
                {
                    Success = true,
                    Token = generateResult.Token,
                    RefreshToken = refreshToken.Token
                };
            }

            return null!;
        }
    }
}
