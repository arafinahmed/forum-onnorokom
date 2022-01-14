using Microsoft.AspNetCore.Authorization;

namespace Onnorokom.Forum.Membership.BusinessObject
{
    public class CommonPermissionRequirement : IAuthorizationRequirement
    {
        public CommonPermissionRequirement()
        {
        }
    }
}