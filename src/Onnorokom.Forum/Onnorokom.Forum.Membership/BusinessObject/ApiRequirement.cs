using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.BusinessObject
{
    public class ApiRequirement : IAuthorizationRequirement
    {
        public ApiRequirement() { }
    }
}
