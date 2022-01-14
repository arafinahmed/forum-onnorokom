using Autofac;
using Onnorokom.Forum.Membership.Services;
using BO = Onnorokom.Forum.Membership.BusinessObject;

namespace Onnorokom.Forum.Web.Models.Comment
{
    public class DeleteCommentModel
    {
        public string? BoardName { get; set; }
        public string? TopicName { get; set; }
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid TopicId { get; set; }
        public string CommentText { get; set; }
        public string? CreatorEmail { get; set; }
        public Guid CreatorId { get; set; }
        private IPostService _postService;
        private ITopicService _topicService;
        private IBoardService _boardService;
        private ICommentService _commentService;
        private IProfileService _profileService;

        public DeleteCommentModel() { }

        public DeleteCommentModel(IPostService postService, ITopicService topicService,
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

        public async Task Load(Guid commentId, Guid userId)
        {
            var user = await _profileService.GetUserByIdAsync(userId);

            if (user == null)
                throw new Exception("No user found.");

            var comment = _commentService.GetComment(commentId);
            if (comment == null)
                throw new Exception("No comment found");


            var post = _postService.GetPost(comment.PostId);

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
            CommentText = comment.CommentText;
            CreatorEmail = comment.CreatorEmail;
            CreatorId = comment.CreatorId;
            Id = comment.Id;
        }

        public async Task Edit(Guid userId)
        {
            var user = await _profileService.GetUserAsync(userId);

            if (user == null)
                throw new FileNotFoundException("User not found with the user id.");

            var claims = await _profileService.GetClaimAsync(user);

            if (claims == null)
                throw new NullReferenceException("Claim is required for delete a comment.");

            var claim = claims.FirstOrDefault();

            if (claim.Type != "Moderator" && claim.Type != "User")
            {
                throw new InvalidOperationException("You are not permited to delete a comment.");
            }

            var comment = _commentService.GetComment(Id);

            if (comment == null)
                throw new FileNotFoundException("No comment found with topic id");

            if (user.Id != comment.CreatorId)
                throw new InvalidOperationException("You are not permitted to delete this comment.");

            await _commentService.Delete(new BO.Comment
            {
                Id = Id,
                CommentText = CommentText,
                CreatorId = CreatorId,
                CreatorEmail = CreatorEmail,
                PostId = PostId
            });
        }
    }
}
