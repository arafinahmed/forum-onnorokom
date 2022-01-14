using Microsoft.EntityFrameworkCore;
using Onnorokom.Forum.DataAccessLayer.Repositories;
using Onnorokom.Forum.Membership.Contexts;
using Onnorokom.Forum.Membership.Entities;

namespace Onnorokom.Forum.Membership.Repositories
{
    public class CommentRepository : Repository<Comment, Guid>, ICommentRepository
    {
        public CommentRepository(IMembershipDbContext context)
            : base((DbContext)context)
        {
        }
    }
}