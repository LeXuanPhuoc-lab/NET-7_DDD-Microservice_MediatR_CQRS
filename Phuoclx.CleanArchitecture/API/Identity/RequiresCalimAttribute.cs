using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Identity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class RequiresCalimAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimName;
        private readonly string[] _claimValues;

        public RequiresCalimAttribute(string claimName, params string[] claimValues)
        {
            _claimName = claimName;
            _claimValues = claimValues;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorize = false;
            foreach(var claim in _claimValues)
            {
                if (context.HttpContext.User.HasClaim(_claimName, claim))
                {
                    isAuthorize = true;
                }
            }

            if(!isAuthorize) context.Result = new ForbidResult();
        }
    }
}
