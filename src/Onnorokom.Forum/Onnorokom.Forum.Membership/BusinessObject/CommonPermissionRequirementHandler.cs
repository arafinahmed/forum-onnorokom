using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.BusinessObject
{
    public class CommonPermissionRequirementHandler :
        AuthorizationHandler<CommonPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               CommonPermissionRequirement requirement)
        {
            var claims = context.User.Claims;

            foreach (var claim in claims.ToList())
            {
                if (claim.Value == "Moderator" || claim.Value == "User")
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}