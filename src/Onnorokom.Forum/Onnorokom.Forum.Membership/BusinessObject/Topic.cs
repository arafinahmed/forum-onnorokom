using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.BusinessObject
{
    public class Topic
    {
        public Guid Id { get; set; }
        public string TopicName { get; set; }
        public Guid BoardId { get; set; }
        public Guid CreatorId { get; set; }
    }
}
