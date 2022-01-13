using Autofac;
using Onnorokom.Forum.Membership.Services;
using BO = Onnorokom.Forum.Membership.BusinessObject;

namespace Onnorokom.Forum.Web.Models.Post
{
    public class CreatePostModel
    {
        public string? BoardName { get; set; }
        public string TopicName { get; set; }
        public Guid TopicId { get; set; }
        public Guid CreatorId { get; set; }
        public string Description { get; set; }
        public string? CreatorEmail { get; set; }
        private ILifetimeScope _scope;
        private IPostService _postService;
        private ITopicService _topicService;
        private IProfileService _profileService;
        private IBoardService _boardService;
        public CreatePostModel() { }

        public CreatePostModel(IPostService postService, ITopicService topicService,
            IProfileService profileService, IBoardService boardService)
        {
            _postService = postService;
            _topicService = topicService;
            _profileService = profileService;
            _boardService = boardService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _boardService = _scope.Resolve<IBoardService>();
            _topicService = _scope.Resolve<ITopicService>();
            _profileService = _scope.Resolve<IProfileService>();
            _postService = _scope.Resolve<IPostService>();
        }

        public void Load(Guid id)
        {
            var topic = _topicService.GetTopic(id);
            if(topic == null)
            {
                BoardName = "404";
                TopicName = "Not Found";
                return;
            }

            var board = _boardService.GetBoard(topic.BoardId);
            if(board == null)
            {
                BoardName = "404";
                TopicName = "Not Found";
                return;
            }

            BoardName = board.BoardName;
            TopicName = topic.TopicName;
            TopicId = topic.Id;
        }

        public async Task CreatePost()
        {
            var user = await _profileService.GetUserAsync(CreatorId);

            if (user == null)
                throw new FileNotFoundException("User not found with the user id");

            var claims = await _profileService.GetClaimAsync(user);
            if (claims == null)
                throw new NullReferenceException("Claim is required for creating a topic");

            var claim = claims.FirstOrDefault();

            if (claim.Type != "Moderator" && claim.Type != "User")
            {
                throw new InvalidOperationException("You are not permited to create a topic");
            }

            var topic = _topicService.GetTopic(TopicId);
            if (topic == null)
                throw new FileNotFoundException("No topic found.");

            await _postService.CreatePost(new BO.Post {Description = Description, TopicId = TopicId, CreatorId = CreatorId, CreatorEmail = CreatorEmail });
        }
    }
}

