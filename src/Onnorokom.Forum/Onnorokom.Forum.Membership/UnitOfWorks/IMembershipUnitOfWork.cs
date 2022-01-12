using Onnorokom.Forum.DataAccessLayer.UnitOfWorks;
using Onnorokom.Forum.Membership.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.UnitOfWorks
{
    public interface IMembershipUnitOfWork : IUnitOfWork
    {
        IBoardRepository Boards { get; }
        ITopicRepository Topics { get; }
    }
}
