using Onnorokom.Forum.Membership.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.Services
{
    public interface IPostService
    {
        Task CreatePost(Post post);
        IList<Post> GetAllPosts(Guid topicId);
    }
}
