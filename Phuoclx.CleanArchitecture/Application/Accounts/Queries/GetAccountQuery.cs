using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.DTOs;

namespace Application.Accounts.Queries
{
    public record GetAccountQuery(string Email) : IRequest<AccountDto>;

    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountDto>
    {
        private readonly IMediator _mediator;

        public GetAccountQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<AccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _mediator.Send(new GetAccountsQuery());
            var account = accounts.FirstOrDefault(x =>
                        x.Email.Equals(request.Email));
            return account != null ? account : null!;
        }
    }
}
