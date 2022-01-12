using Microsoft.AspNetCore.Identity;
using Onnorokom.Forum.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.Services
{
    public class ProfileService : IProfileService
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public ProfileService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> GetUserAsync(string userName)
        {
            if (String.IsNullOrWhiteSpace(userName))
                throw new InvalidOperationException("User name must be provided to get a user.");

            return await _userManager.FindByEmailAsync(userName);
        }

        public async Task<ApplicationUser> GetUserAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new InvalidOperationException("User id must be provided to get a user.");

            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<IList<Claim>> GetClaimAsync(ApplicationUser user)
        {
            if (user == null)
                throw new InvalidOperationException("User must be provided to get user claims.");

            var oldUser = await GetUserAsync(user.UserName);

            if (oldUser == null)
                throw new InvalidOperationException("User must be provided to get user claims.");

            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new InvalidOperationException("No user found with the user id.");

            return await _userManager.FindByIdAsync(userId.ToString());
        }
    }
}
