using Application.Identity.Commands.SignUp;

namespace API.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class AuthenticationController : ControllerBase 
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(APIRoutes.Identity.SignIn)]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var result = await _mediator.Send(new SignInQuery(username, password));

            return result.Match<IActionResult>(x => {
                if (x == null)
                    return BadRequest(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Wrong username or password"
                    });
                return Ok(new AuthSucessResponse { 
                    Token = x.Token,
                    RefreshToken = x.RefreshToken
                });
            }, exception => {
                if (exception is ValidationException validationException) 
                {
                    return BadRequest(validationException.ToProblemDetails());
                }

                return StatusCode(StatusCodes.Status500InternalServerError);
            });


            //if (result.IsFaulted)
            //    return result.ToResponse(x => x);
            //else
            //    return result.ToResponse(x => {
            //        if (x == null)
            //            return new BaseResponse {
            //                StatusCode = StatusCodes.Status400BadRequest,
            //                Message = "Wrong username or password"
            //            };
            //        return x!.ToSuccessBaseResponse("Sign in successfully");
            //    });
        }

        [HttpPost(APIRoutes.Identity.SignUp)]
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

        [HttpPost(APIRoutes.Identity.Refresh)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request) 
        {
            var authResponse = 
                await _mediator.Send(new CreateRefreshTokenCommand(request.Token, request.RefreshToken));

            return authResponse.Match<IActionResult>(x =>
            {
                if (!x.Success)
                    return BadRequest(new AuthFailedResponse { 
                        Errors = x.Errors
                    });

                return Ok(new AuthSucessResponse { 
                    Token = x.Token,
                    RefreshToken = x.RefreshToken,  
                });
            }, exception => {
                if(exception is ValidationException validationException)
                {
                    return BadRequest(validationException.ToProblemDetails());
                }

                return StatusCode(500);
            });
        }
    }
}
