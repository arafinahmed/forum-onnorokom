using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.BusinessObject
{
    public class UserRequirementHandler : AuthorizationHandler<ModeratorRequirement>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               ModeratorRequirement requirement)
        {
            var claim = context.User.FindFirst("User");

            if (claim != null && bool.Parse(claim.Value))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}