using Autofac;
using Onnorokom.Forum.Membership.Services;
using BO = Onnorokom.Forum.Membership.BusinessObject;

namespace Onnorokom.Forum.Web.Models.Home
{
    public class LoadAllCommentsModel
    {
        public string? BoardName { get; set; }
        public string? TopicName { get; set; }
        public Guid PostId { get; set; }
        public Guid TopicId { get; set; }
        public string? Description { get; set; }
        public string? CreatorEmail { get; set; }
        public string CommentText { get; set; }
        public IList<BO.Comment> Comments { get; set; }
        private IPostService _postService;
        private ITopicService _topicService;
        private IBoardService _boardService;
        private ICommentService _commentService;

        public LoadAllCommentsModel() { }

        public LoadAllCommentsModel(IPostService postService, ITopicService topicService,
            IBoardService boardService, ICommentService commentService)
        {
            _postService = postService;
            _topicService = topicService;
            _boardService = boardService;
            _commentService = commentService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _boardService = scope.Resolve<IBoardService>();
            _topicService = scope.Resolve<ITopicService>();
            _postService = scope.Resolve<IPostService>();
            _commentService = scope.Resolve<ICommentService>();
        }

        public void Load(Guid id)
        {
            Comments = new List<BO.Comment>();
            var post = _postService.GetPost(id);

            if (post == null)
            {
                BoardName = "404";
                TopicName = "Not Found";
                return;
            }

            var topic = _topicService.GetTopic(post.TopicId);

            if (topic == null)
            {
                BoardName = "404";
                TopicName = "Not Found";
                return;
            }

            var board = _boardService.GetBoard(topic.BoardId);

            if (board == null)
            {
                BoardName = "404";
                TopicName = "Not Found";
                return;
            }

            BoardName = board.BoardName;
            TopicName = topic.TopicName;
            TopicId = topic.Id;
            PostId = post.Id;
            Description = post.Description;
            CreatorEmail = post.CreatorEmail;
            Comments = _commentService.GetComments(PostId);
        }
    }
}