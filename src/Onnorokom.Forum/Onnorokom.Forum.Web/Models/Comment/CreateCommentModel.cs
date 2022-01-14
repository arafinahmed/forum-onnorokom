using Autofac;
using Onnorokom.Forum.Membership.Services;
using BO = Onnorokom.Forum.Membership.BusinessObject;

namespace Onnorokom.Forum.Web.Models.Comment
{
    public class CreateCommentModel
    {
        public Guid PostId { get; set; }
        public string CommentText { get; set; }
        public string CreatorEmail { get; set; }
        public Guid CreatorId { get; set; }
        private IPostService _postService;
        private ITopicService _topicService;
        private IBoardService _boardService;
        private ICommentService _commentService;
        private IProfileService _profileService;

        public CreateCommentModel() { }

        public CreateCommentModel(IPostService postService, ITopicService topicService,
            IBoardService boardService, ICommentService commentService, IProfileService profileService)
        {
            _postService = postService;
            _topicService = topicService;
            _boardService = boardService;
            _commentService = commentService;
            _profileService = profileService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _boardService = scope.Resolve<IBoardService>();
            _topicService = scope.Resolve<ITopicService>();
            _postService = scope.Resolve<IPostService>();
            _commentService = scope.Resolve<ICommentService>();
            _profileService = scope.Resolve<IProfileService>();
        }

        public async Task CreateComment(Guid userId)
        {
            var user = await _profileService.GetUserAsync(userId);

            if (user == null)
                throw new FileNotFoundException("User not found with the user id");

            var claims = await _profileService.GetClaimAsync(user);

            if (claims == null)
                throw new NullReferenceException("Claim is required for creating a comment");

            var claim = claims.FirstOrDefault();

            if (claim.Type != "Moderator" && claim.Type != "User")
            {
                throw new InvalidOperationException("You are not permited to create a comment");
            }

            var post = _postService.GetPost(PostId);

            if (post == null)
                throw new FileNotFoundException("No post found.");

            await _commentService.CreateComment(new BO.Comment { CommentText = CommentText, PostId = PostId, CreatorId = CreatorId, CreatorEmail = CreatorEmail });
        }
    }
}