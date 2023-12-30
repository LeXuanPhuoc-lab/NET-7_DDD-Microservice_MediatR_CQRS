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
            return result.ToResponse(res => res);
        }

        //[HttpPost("sign-up")]
        //public Task<IActionResult> SignUp() 
        //{
        
        //}


    }
}
