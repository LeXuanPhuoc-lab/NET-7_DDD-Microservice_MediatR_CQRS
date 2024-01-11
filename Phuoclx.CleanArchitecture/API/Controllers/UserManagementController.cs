using Application.Identity.Commands.CreateUser;
using Application.Identity.Queries;
using Application.Roles.Queries;
using Domain.Entities;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    public class UserManagementController : ControllerBase 
    {
        private readonly IMediator _mediator;

        public UserManagementController(IMediator mediator)
        {
            _mediator = mediator; 
        }

        /// <summary>
        /// Get All User
        /// </summary>
        /// <returns></returns>
        [HttpGet(APIRoutes.UserManagement.GetAll)]
        [RequiresCalim(IdentityData.RoleClaimName, new[] { UserRoles.Administrator})]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _mediator.Send(new GetAllUserQuery());

            return result.ToResponse<IEnumerable<ApplicationUser>, IActionResult>(x => {
                if (x != null || x?.Count() > 0)
                    return Ok(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Data = x
                    });

                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Not found any users"
                });
            });
        }

        /// <summary>
        /// Get All role
        /// </summary>
        /// <returns></returns>
        [HttpGet(APIRoutes.Roles.GetAll)]
        //[RequiresCalim(IdentityData.RoleClaimName, new[] { UserRoles.Administrator, UserRoles.Manager})]
        public async Task<IActionResult> GetAllRole()
        {
            var result = await _mediator.Send(new GetAllRoleQuery());

            return result.ToResponse<IEnumerable<Role>, IActionResult>(x => { 
                if(x != null || x?.Count() > 0)
                    return Ok(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Data = x
                    });

                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Not found any role"
                });
            });
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(APIRoutes.UserManagement.Create)]
        //[RequiresCalim(IdentityData.RoleClaimName, new[] { UserRoles.Administrator})]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var result = await _mediator.Send(new CreateUserCommand(request.UserName,
                request.Password, request.Role, request.DateOfBirth, request.IdentityCardNumber));

            return result.ToResponse<ApplicationUser, IActionResult>(x => { 
                if(x != null)
                    return Ok(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Create successfully",
                        Data = x
                    });

                return StatusCode(StatusCodes.Status500InternalServerError);
            });
        }

    }
}
