using Onnorokom.Forum.Membership.BusinessObject;

namespace Onnorokom.Forum.Membership.Services
{
    public interface IPostService
    {
        Task CreatePost(Post post);
        IList<Post> GetAllPosts(Guid topicId);
        Post GetPost(Guid id);
        Task UpdatePostDescription(Post post);
        Task DeletePost(Post post);
    }
}
