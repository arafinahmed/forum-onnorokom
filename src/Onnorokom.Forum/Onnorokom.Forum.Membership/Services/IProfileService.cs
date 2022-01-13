using Onnorokom.Forum.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.Services
{
    public interface IProfileService
    {
        Task<ApplicationUser> GetUserAsync(string userName);
        Task<IList<Claim>> GetClaimAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserAsync(Guid userId);
        Task<ApplicationUser> GetUserByIdAsync(Guid userId);
    }
}