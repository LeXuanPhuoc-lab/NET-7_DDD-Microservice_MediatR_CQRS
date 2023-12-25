using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("accounts")]
        public async Task<IEnumerable<AccountDto>> GetAccounts() 
        {
            var test =  await _mediator.Send(new GetAccountsQuery());
            return test;
        }
    }
}
