using Onnorokom.Forum.Membership.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.Services
{
    public interface ITopicService
    {
        Task CreateTopic(Topic topic, Guid userId);
        IList<Topic> GetAllTopics(Guid boardId);
        Topic GetTopic(Guid id);
        Task UpdateTopicName(Topic topic);
    }
}
