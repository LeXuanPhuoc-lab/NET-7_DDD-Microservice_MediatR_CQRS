namespace Application.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("sign-in")]
        public async Task<IActionResult> SignIn(string username, string password) 
        {
            //var response = await _mediator.Send(new SignInQuery(username, password));

            //if (response is null)
            //    return BadRequest();

            //return Ok(response);
            return Ok();
        }

        //[HttpPost("sign-up")]
        //public async Task<IActionResult> SignUp(SignUpCommand request) 
        //{
        //    var result = await _mediator.Send(request);

        //    return result.ToOk(async u => await _mediator.Send(new SignInQuery(u.UserName!, request.Password)));
        //}

        //[Authorize(Roles = Roles.Administrator)]
        //[HttpGet("accounts")]
        //public async Task<IActionResult> GetAccounts() 
        //{
        //    return Ok(await _mediator.Send(new GetAccountsQuery()));
        //}

        //[HttpGet("accounts/{email}")]
        //public async Task<IActionResult> GetAccountByEmail([FromRoute] string email) 
        //{
        //    return Ok(await _mediator.Send(new GetAccountQuery(email)));
        //}


        //[HttpPost("accounts")]
        //public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand request) 
        //{
        //    return Ok(await _mediator.Send(request));
        //}

        //[HttpPut("accounts")]
        //public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountCommand request) 
        //{
        //    return Ok(await _mediator.Send(request));
        //}
    }
}
