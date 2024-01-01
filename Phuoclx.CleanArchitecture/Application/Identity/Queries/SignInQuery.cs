using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Runtime.InteropServices;

namespace Application.Accounts.Queries
{
    public record SignInQuery(string Username, string Password) : IRequest<Result<string>>;

    public class SignInQueryHandler : IRequestHandler<SignInQuery, Result<string>>
    {
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly IIdentityRepository _identityRepo;

        //private readonly IIdentityService _identityService;

        private readonly AppSettings _appSettings;

        public SignInQueryHandler(SignInManager<ApplicationUser> signInManger,
            RoleManager<IdentityRole> roleManager,
            //IIdentityService identityService,
            IIdentityRepository identityRepo,
            IOptionsMonitor<AppSettings> monitor) 
        {
            _signInManger = signInManger;
            //_identityService = identityService;
            _identityRepo = identityRepo;
            _appSettings = monitor.CurrentValue;
        }

        public async Task<Result<string>> Handle(SignInQuery request, 
            CancellationToken cancellationToken)
        {
            // validation
            var validator = new SignInQueryValidator();
            var validateResult = await validator.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                ValidationException exception = new(validateResult.Errors);
                return new Result<string>(exception);
            }

            var user = await _identityRepo.GetUserNameAsync(request.Username);
            if (user != null) 
            {
                var roles = await _identityRepo.GetRolesbyUserIdAsync(user.Id);

                await _signInManger.SignInAsync(user, isPersistent: false);

                // Generate token
                return new JwtHelper(_appSettings).GenerateToken(user,
                    roles != null ? roles.ToArray() : null!);
            }

            return null!;
        }
    }
}
