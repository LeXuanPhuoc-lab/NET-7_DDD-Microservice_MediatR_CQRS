using Infrastructure.Repository;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Identity.Commands.CreateUser
{
    public record CreateUserCommand(string Username, string Password,
        string Role, DateTime DateOfBirth, string? IdentityCardNumber) : IRequest<Result<ApplicationUser>>;

    public class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, Result<ApplicationUser>>
    {
        private readonly IDriverLicenseLearningSupportContext _context;
        private readonly IIdentityRepository _identityRepo;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateUserCommandHandler(IDriverLicenseLearningSupportContext context,
            IIdentityRepository identityRepository,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _identityRepo = identityRepository;
            _roleManager = roleManager;
        }

        public async Task<Result<ApplicationUser>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Validation <- ValidatorBehaviour

            // Check role exist
            var role = await _roleManager.FindByNameAsync(request.Role);
            if (role == null) return new Result<ApplicationUser>(new Exception("Not found any role"));

            // Create User
            var applicationUser = new ApplicationUser()
            {
                UserName = request.Username,
                Email = request.Username,   
                Dob = request.DateOfBirth,
                IdentityCardNumber = request.IdentityCardNumber
            };

            await _identityRepo.CreateAsync(applicationUser, request.Password, request.Role);
            return applicationUser;
        }
    }
}
