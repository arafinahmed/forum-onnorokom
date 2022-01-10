using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.BusinessObject
{
    public class ViewRequirementHandler :
        AuthorizationHandler<ViewRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            ViewRequirement requirement)
        {
            var claim = context.User.FindFirst("view_permission");
            if(claim != null && bool.Parse(claim.Value))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
