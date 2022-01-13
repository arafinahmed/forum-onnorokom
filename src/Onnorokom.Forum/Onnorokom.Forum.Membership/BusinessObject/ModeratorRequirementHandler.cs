using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.BusinessObject
{
    public class ModeratorRequirementHandler : AuthorizationHandler<ModeratorRequirement>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               ModeratorRequirement requirement)
        {
            var claim = context.User.FindFirst("Moderator");

            if (claim != null && bool.Parse(claim.Value))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}