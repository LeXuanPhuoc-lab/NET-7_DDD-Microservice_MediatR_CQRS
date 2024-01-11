using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Roles.Queries
{
    public record GetAllRoleQuery : IRequest<Result<IEnumerable<Role>>>;

    public class GetRolesQueryHandler : IRequestHandler<GetAllRoleQuery, Result<IEnumerable<Role>>>
    {
        private readonly IDriverLicenseLearningSupportContext _context;

        public GetRolesQueryHandler(IDriverLicenseLearningSupportContext context)
            => _context = context;

        public async Task<Result<IEnumerable<Role>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
