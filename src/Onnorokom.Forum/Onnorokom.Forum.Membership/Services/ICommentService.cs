using Onnorokom.Forum.Membership.BusinessObject;

namespace Onnorokom.Forum.Membership.Services
{
    public interface ICommentService
    {
        Task CreateComment(Comment comment);
        IList<Comment> GetComments(Guid postId);
    }
}