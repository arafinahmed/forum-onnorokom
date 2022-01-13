namespace Onnorokom.Forum.Membership.BusinessObject
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid TopicId { get; set; }
        public Guid CreatorId { get; set; }
        public string CreatorEmail { get; set; }
    }
}
