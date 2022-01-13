using Microsoft.AspNetCore.Identity;
using Onnorokom.Forum.Membership.Entities;

namespace Onnorokom.Forum.Membership.Seeds
{
    public static class DataSeed
    {
        public static Role[] Roles
        {
            get
            {
                return new Role[]
                {
                    new Role { Id = Guid.NewGuid(), Name = "Moderator", NormalizedName = "MODERATOR", ConcurrencyStamp = Guid.NewGuid().ToString() },
                    new Role { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER", ConcurrencyStamp = Guid.NewGuid().ToString() }
                };
            }
        }
    }
}
