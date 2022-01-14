using Autofac;
using Microsoft.AspNetCore.Identity;
using Onnorokom.Forum.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.Seeds
{
    public class ModeratorDataSeed
    {
        private UserManager<ApplicationUser> _userManager;

        public ModeratorDataSeed() { }

        public ModeratorDataSeed(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
        }

        public async Task SeedUserAsync()
        {
            var modUser = new ApplicationUser
            {
                UserName = "moderator@email.com",
                Email = "moderator@email.com",
                EmailConfirmed = true
            };

            IdentityResult result = null;
            var password = "moderator@email.com";

            if (await _userManager.FindByEmailAsync(modUser.Email) == null)
            {
                result = await _userManager.CreateAsync(modUser, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(modUser, "Moderator");
                    await _userManager.AddClaimAsync(modUser, new Claim("Moderator", "true"));
                }
            }
        }
    }
}