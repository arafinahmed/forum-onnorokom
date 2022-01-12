using Microsoft.AspNetCore.Identity;
using Onnorokom.Forum.Membership.Entities;

namespace Onnorokom.Forum.Membership.Seeds
{
    public static class DataSeed
    {
        private static Guid _modId = new Guid("4ba21d17-018a-4c06-965c-632ee43c8f2d");
        private static Guid _userId = new Guid("a6f92de9-f845-435e-8890-283e30babfb1");
        private static ApplicationUser user = new ApplicationUser { Id = _userId, UserName = "mod@email.com", Email = "mod@email.com", EmailConfirmed = true };
        public static Role[] Roles
        {
            get
            {
                return new Role[]
                {
                    new Role { Id = _modId, Name = "Moderator", NormalizedName = "MODERATOR", ConcurrencyStamp = Guid.NewGuid().ToString() },
                    new Role { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER", ConcurrencyStamp = Guid.NewGuid().ToString() }
                };
            }
        }
        public static ApplicationUser[] Moderator
        {
            get
            {
                return new ApplicationUser[]
                {
                    new ApplicationUser { Id = _userId, UserName = "mod@email.com", Email = "mod@email.com",
                        PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, "mod@email.com"),
                        EmailConfirmed = true}
                };
            }
        }
        public static UserRole[] UserRole
        {
            get
            {
                return new UserRole[]
                {
                    new UserRole { RoleId = _modId, UserId = _userId}
                };
            }
        }
        public static UserClaim[] UserClaims
        {
            get
            {
                return new UserClaim[]
                {
                    new UserClaim {Id=1, UserId = _userId, ClaimType = "Moderator", ClaimValue = "true"}
                };
            }
        }
    }
}
