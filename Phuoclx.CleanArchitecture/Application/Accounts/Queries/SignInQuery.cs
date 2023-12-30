using Microsoft.AspNetCore.Mvc.Formatters;
using System;

namespace Application.Accounts.Queries
{
    public record SignInQuery(string Username, string Password) : IRequest<Result<BaseResponse>>;

    public class SignInQueryHandler : IRequestHandler<SignInQuery, Result<BaseResponse>>
    {
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly IIdentityService _identityService;
        private readonly AppSettings _appSettings;

        public SignInQueryHandler(SignInManager<ApplicationUser> signInManger,
            RoleManager<IdentityRole> roleManager,
            IIdentityService identityService,
            IOptionsMonitor<AppSettings> monitor) 
        {
            _signInManger = signInManger;
            _identityService = identityService;
            _appSettings = monitor.CurrentValue;
        }

        public async Task<Result<BaseResponse>> Handle(SignInQuery request, 
            CancellationToken cancellationToken)
        {
            // validation
            var validator = new SignInQueryValidator();
            var validateResult = await validator.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                ValidationException exception = new(validateResult.Errors);
                return new Result<BaseResponse>(exception);
            }

            var user = await _identityService.GetByEmailAsync(request.Username);
            if(user is not null) 
            {
                var roles = await _identityService.GetRolesByUserIdAsync(user.Id);

                await _signInManger.SignInAsync(user, isPersistent: false);

                /// Generate token
                var token = new JwtHelper(_appSettings).GenerateToken(user,
                    roles != null ? roles.ToArray() : null!);

                return new BaseResponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Login successfully",
                    Data = token
                };
            }
            return new BaseResponse()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Wrong username or password"
            };
        }
    }
}
