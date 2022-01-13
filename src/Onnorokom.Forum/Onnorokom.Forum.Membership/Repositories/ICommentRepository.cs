﻿using Onnorokom.Forum.DataAccessLayer.Repositories;
using Onnorokom.Forum.Membership.Entities;

namespace Onnorokom.Forum.Membership.Repositories
{
    public interface ICommentRepository : IRepository<Comment, Guid>
    {
    }
}
