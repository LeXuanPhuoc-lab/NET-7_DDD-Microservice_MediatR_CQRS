using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.Accounts.Queries
{
    public record GetAccountsQuery : IRequest<IEnumerable<AccountDto>>;

    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, IEnumerable<AccountDto>>
    {
        private readonly IDriverLicenseLearningSupportContext _context;
        private readonly IMapper _mapper;

        public GetAccountsQueryHandler(IDriverLicenseLearningSupportContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDto>> Handle(GetAccountsQuery request,
            CancellationToken cancellationToken)
        {
            return _context.Accounts.ProjectTo<AccountDto>(
                _mapper.ConfigurationProvider);
        }
    }
}
