using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Application.Accounts.Queries
{
    //public record GetAccountsQuery : IRequest<Result<IEnumerable<AccountDto>>>;

    //public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, Result<IEnumerable<AccountDto>>>
    //{
    //    private readonly IDriverLicenseLearningSupportContext _context;
    //    private readonly IMapper _mapper;

    //    public GetAccountsQueryHandler(IDriverLicenseLearningSupportContext context,
    //        IMapper mapper)
    //    {
    //        _context = context;
    //        _mapper = mapper;
    //    }

    //    public async Task<Result<IEnumerable<AccountDto>>> Handle(GetAccountsQuery request,
    //        CancellationToken cancellationToken)
    //    {
    //        var accounts = await _context.Accounts
    //                             .ProjectTo<AccountDto>(_mapper.ConfigurationProvider)
    //                             .ToListAsync();
    //        return accounts!.Count > 0 ? accounts : null!;
    //    }
    //}

    public record GetAccountsQuery : IRequest<IEnumerable<ApplicationUser>>;

    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, IEnumerable<ApplicationUser>>
    {
        private readonly IIdentityRepository _identityRepository; // CachedIdentityRepository
        private readonly IMapper _mapper;

        public GetAccountsQueryHandler(IIdentityRepository identityRepository,
            IMapper mapper)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationUser>> Handle(GetAccountsQuery request,
            CancellationToken cancellationToken)
        {
            return await _identityRepository.GetUsersAsync();
        }
    }
}
