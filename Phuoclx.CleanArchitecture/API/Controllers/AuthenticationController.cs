using Application.Accounts.Commands;
using Application.Common.Extensions;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase 
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("sign-in")]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var result = await _mediator.Send(new SignInQuery(username, password));

            if (result.IsFaulted)
                return result.ToResponse(x => x);
            else
                return result.ToResponse(x => {
                    if (x == null)
                        return new BaseResponse {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Message = "Wrong username or password"
                        };
                    return x!.ToSuccessBaseResponse("Sign in successfully");
                });
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpCommand request)
        {
           var result = await _mediator.Send(request);

            if (result.IsFaulted)
                return result.ToResponse(x => x);
            else
                return result.ToResponse(x => { 
                    if(x == null) 
                        return new BaseResponse (){ 
                            StatusCode = StatusCodes.Status500InternalServerError };
                    return x!.ToSuccessBaseResponse("Sign up successfully");
                });
        }

        [HttpGet("accounts")]
        [Authorize]
        //[RequiresCalim(IdentityData.RoleClaimName, new[] { Roles.Administrator , Roles.Manager})]
        public async Task<IActionResult> GetAccounts()
        {
            //var result = await _mediator.Send(new GetAccountsQuery());

            //if (result.IsFaulted)
            //    return result.ToResponse(x => x);
            //else
            //    return result.ToResponse(x =>
            //    {
            //        if (x == null)
            //            return new BaseResponse
            //            {
            //                StatusCode = StatusCodes.Status404NotFound,
            //                Message = "Not found any account"
            //            };
            //        return x!.ToSuccessBaseResponse();
            //    });

            var result = await _mediator.Send(new GetAccountsQuery());

            return Ok(result);
        }
    }
}
