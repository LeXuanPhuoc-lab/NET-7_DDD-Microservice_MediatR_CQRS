using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Identity.Queries
{
    public record GetAllUserQuery 
        : IRequest<Result<IEnumerable<ApplicationUser>>>;

    public class GetUsersQueryHandler
        : IRequestHandler<GetAllUserQuery, Result<IEnumerable<ApplicationUser>>>
    {
        private readonly IDriverLicenseLearningSupportContext _context;

        public GetUsersQueryHandler(IDriverLicenseLearningSupportContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<ApplicationUser>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync();
        }
    }

}
