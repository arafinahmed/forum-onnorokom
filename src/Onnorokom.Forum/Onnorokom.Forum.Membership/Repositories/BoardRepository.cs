using Microsoft.EntityFrameworkCore;
using Onnorokom.Forum.DataAccessLayer.Repositories;
using Onnorokom.Forum.Membership.Contexts;
using Onnorokom.Forum.Membership.Entities;

namespace Onnorokom.Forum.Membership.Repositories
{
    public class BoardRepository : Repository<Board, Guid>, IBoardRepository
    {
        public BoardRepository(IMembershipDbContext context)
            : base((DbContext)context)
        {
        }
    }
}