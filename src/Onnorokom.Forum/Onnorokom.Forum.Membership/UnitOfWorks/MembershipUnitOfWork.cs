using Microsoft.EntityFrameworkCore;
using Onnorokom.Forum.DataAccessLayer.UnitOfWorks;
using Onnorokom.Forum.Membership.Contexts;
using Onnorokom.Forum.Membership.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.UnitOfWorks
{
    public class MembershipUnitOfWork : UnitOfWork, IMembershipUnitOfWork
    {
        public IBoardRepository Boards { get; private set; }
        public ITopicRepository Topics { get; private set; }
        public IPostRepository Posts { get; private set; }
        public ICommentRepository Comments { get; private set; }

        public MembershipUnitOfWork(IMembershipDbContext context,
            IBoardRepository board,
            ITopicRepository topics,
            IPostRepository posts,
            ICommentRepository comments
            ) : base((DbContext)context)
        {
            Boards = board;
            Topics = topics;
            Posts = posts;
            Comments = comments;
        }
    }
}