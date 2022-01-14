using Autofac;
using BO = Onnorokom.Forum.Membership.BusinessObject;
using Onnorokom.Forum.Membership.Services;

namespace Onnorokom.Forum.Web.Models.Topic
{
    public class CreateTopicModel
    {
        public string? BoardName { get; set; }
        public string TopicName { get; set; }
        public Guid BoardId { get; set; }
        private ILifetimeScope _scope;
        private IBoardService _boardService;
        private ITopicService _topicService;
        private IProfileService _profileService;

        public CreateTopicModel() { }

        public CreateTopicModel(IBoardService boardService, ITopicService topicService,
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

        public void LoadBoard(Guid id)
        {
            var board = _boardService.GetBoard(id);

            if (board == null)
            {
                BoardName = "No board found.";
                return;
            }
            BoardId = board.Id;
            BoardName = board.BoardName;
        }

        public async Task CreateTopic(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
                throw new InvalidOperationException("User Email Must Be Provided For creating a topic");

            var user = await _profileService.GetUserAsync(userEmail);

            if (user == null)
                throw new FileNotFoundException("User not found with the email id");

            var claims = await _profileService.GetClaimAsync(user);

            if (claims == null)
                throw new NullReferenceException("Claim is required for creating a topic");

            var claim = claims.FirstOrDefault();

            if (claim.Type != "Moderator" && claim.Type != "User")
            {
                throw new InvalidOperationException("You are not permited to create a topic");
            }

            await _topicService.CreateTopic(new BO.Topic {TopicName = TopicName, BoardId = BoardId }, user.Id);
        }
    }
}