using Onnorokom.Forum.DataAccessLayer.Repositories;
using Onnorokom.Forum.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.Repositories
{
    public interface IBoardRepository : IRepository<Board, Guid>
    {
    }
}
