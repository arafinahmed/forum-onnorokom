using Autofac;
using Onnorokom.Forum.Membership.Services;
using BO = Onnorokom.Forum.Membership.BusinessObject;

namespace Onnorokom.Forum.Web.Models.Home
{
    public class LoadAllPostsModel
    {
        public string? BoardName { get; set; }
        public string TopicName { get; set; }
        public Guid TopicId { get; set; }
        public IList<BO.Post> Posts { get; set;}
        private ILifetimeScope _scope;
        private IPostService _postService;
        private ITopicService _topicService;
        private IBoardService _boardService;
        public LoadAllPostsModel() { }

        public LoadAllPostsModel(IPostService postService, ITopicService topicService,
            IBoardService boardService)
        {
            _postService = postService;
            _topicService = topicService;
            _boardService = boardService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _boardService = _scope.Resolve<IBoardService>();
            _topicService = _scope.Resolve<ITopicService>();
            _postService = _scope.Resolve<IPostService>();
        }

        public void Load(Guid id)
        {
            var topic = _topicService.GetTopic(id);
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

            Posts = _postService.GetAllPosts(TopicId);
        }
    }
}
