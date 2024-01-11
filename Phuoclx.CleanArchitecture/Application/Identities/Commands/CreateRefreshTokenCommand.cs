namespace Application.Identity.Commands
{
    public record CreateRefreshTokenCommand(string Token, string RefreshToken) 
        : IRequest<Result<AuthenticationResult>>;

    public class CreateRefreshTokenCommandHandler
        : IRequestHandler<CreateRefreshTokenCommand, Result<AuthenticationResult>>
    {
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IDriverLicenseLearningSupportContext _context;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly AppSettings _appSettings;

        public CreateRefreshTokenCommandHandler(TokenValidationParameters tokenValidationParameters, IDriverLicenseLearningSupportContext context,
          IOptionsMonitor<AppSettings> monitor,
          IRefreshTokenRepository refreshTokenRepository,
          IIdentityRepository identityRepository)
        {
            _tokenValidationParameters = tokenValidationParameters;
            _context = context;
            _identityRepository = identityRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _appSettings = monitor.CurrentValue;
        }

        public async Task<Result<AuthenticationResult>> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var validatedToken = GetPricipalFromToken(request.Token);

            if (validatedToken == null)
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };

            //var expiryDateUnix 
            //    = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            //var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            //    .AddSeconds(expiryDateUnix);

            var expiryDateTimeUnix = 
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = DateTimeOffset.FromUnixTimeSeconds(expiryDateTimeUnix).UtcDateTime;


            if (expiryDateTimeUtc > DateTime.UtcNow)
                return new AuthenticationResult { Errors = new[] { "The token hasn't expired yet." } };
            
            
            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.JwtId == request.RefreshToken);

            if(storedRefreshToken == null)
                return new AuthenticationResult { Errors = new[] { "The refresh token does not exist" } };

            if(DateTime.UtcNow > storedRefreshToken.ExpiryDate)  
                return new AuthenticationResult { Errors = new[] { "The refresh token has expired" } };

            if(storedRefreshToken.JwtId != jti) 
                return new AuthenticationResult { Errors = new[] { "The refresh token does not match JWT" } };

            if(storedRefreshToken.InValidated)
                return new AuthenticationResult { Errors = new[] { "The refresh token has been invalidated" } };

            if(storedRefreshToken.Used)
                return new AuthenticationResult { Errors = new[] { "The refresh token has been used" } };


            // update old refresh token has been used
            storedRefreshToken.Used = true;
            _context.RefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangeAsync(cancellationToken);

            // generate new token and refresh token for user 
            var user = await _identityRepository.GetByIdAsync(validatedToken.Claims.Single(x => x.Type == "userId").Value);
            var roles = await _identityRepository.GetRolesbyUserIdAsync(user!.Id);
            var generateResult = new JwtHelper(_appSettings).GenerateTokenWithTokenId(user, roles != null ? roles.ToArray() : null!);
            var refreshToken = await _refreshTokenRepository.GenerateRefreshTokenForUserAsync(user, generateResult.TokenId);

            return new AuthenticationResult() 
            {
                Success = true,
                Token = generateResult.Token,
                RefreshToken = refreshToken.Token
            };
        }

        // Get token valid princial 
        private ClaimsPrincipal GetPricipalFromToken(string token) 
        {
            // token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // validate token 
            try
            {
                var principal =  tokenHandler.ValidateToken(token, _tokenValidationParameters,
                    out var validatedToken);

                if(!IsJWTWithValidSecurityAlthorithm(validatedToken)) { return null!; }

                return principal;
            }
            catch
            {
                return null!;
            }
        }

        // Is Valid Security Althorithm
        private bool IsJWTWithValidSecurityAlthorithm(SecurityToken validatedToken) 
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512,
                    StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
