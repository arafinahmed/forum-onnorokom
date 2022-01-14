using Autofac;
using Onnorokom.Forum.Membership.Services;
using BO = Onnorokom.Forum.Membership.BusinessObject;

namespace Onnorokom.Forum.Web.Models.Topic
{
    public class EditTopicModel
    {
        public Guid Id { get; set; }
        public Guid BoardId { get; set; }
        public Guid CreatorId { get; set; }
        public string? BoardName { get; set; }
        public string TopicName { get; set; }
        private ILifetimeScope _scope;
        private IBoardService _boardService;
        private ITopicService _topicService;
        private IProfileService _profileService;

        public EditTopicModel() { }

        public EditTopicModel(IBoardService boardService, ITopicService topicService,
            IProfileService profileService)
        {
            _boardService = boardService;
            _topicService = topicService;
            _profileService = profileService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _boardService = _scope.Resolve<IBoardService>();
            _topicService = _scope.Resolve<ITopicService>();
            _profileService = _scope.Resolve<IProfileService>();
        }

        public void LoadTopic(Guid id)
        {
            var topic = _topicService.GetTopic(id);

            if (topic == null)
            {
                BoardName = "No topic found.";
                return;
            }

            var board = _boardService.GetBoard(topic.BoardId);

            if (board == null)
            {
                BoardName = "No board found.";
                return;
            }

            BoardId = board.Id;
            BoardName = board.BoardName;
            Id = topic.Id;
            TopicName = topic.TopicName;
            CreatorId = topic.CreatorId;
        }

        public async Task EditTopic()
        {
            var user = await _profileService.GetUserAsync(CreatorId);

            if (user == null)
                throw new FileNotFoundException("User not found with the creator id.");

            var claims = await _profileService.GetClaimAsync(user);

            if (claims == null)
                throw new NullReferenceException("Claim is required for creating a topic");

            var claim = claims.FirstOrDefault();

            if (claim.Type != "Moderator" && claim.Type != "User")
            {
                throw new InvalidOperationException("You are not permited to create a topic");
            }

            var topic = _topicService.GetTopic(Id);

            if (topic == null)
                throw new FileNotFoundException("No topic found with topic id");

            if (user.Id != topic.CreatorId)
                throw new InvalidOperationException("You are not permitted to change the topic name");

            await _topicService.UpdateTopicName(new BO.Topic { TopicName = TopicName, BoardId = BoardId, CreatorId = CreatorId, Id = Id });
        }
    }
}