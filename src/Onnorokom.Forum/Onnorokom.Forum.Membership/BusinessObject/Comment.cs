namespace Onnorokom.Forum.Membership.BusinessObject
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string CommentText { get; set; }
        public string CreatorEmail { get; set; }
        public Guid CreatorId { get; set; }
    }
}