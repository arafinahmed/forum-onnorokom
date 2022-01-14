using Onnorokom.Forum.Membership.BusinessObject;
using EO = Onnorokom.Forum.Membership.Entities;
using Onnorokom.Forum.Membership.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMembershipUnitOfWork _unitOfWork;
        private readonly IProfileService _profileService;

        public CommentService(IMembershipUnitOfWork unitOfWork, IProfileService profileService)
        {
            _unitOfWork = unitOfWork;
            _profileService = profileService;
        }

        public async Task CreateComment(Comment comment)
        {
            ArgumentNullException.ThrowIfNull(comment, "No comment provided");
            //if (comment == null)
            //    throw new ArgumentNullException("No comment provided");

            var user = await _profileService.GetUserByIdAsync(comment.CreatorId);

            if (user == null)
                throw new FileNotFoundException("User not found with the user id");

            if (user.Email != comment.CreatorEmail)
                throw new InvalidOperationException("Comment creator email not match with user email");

            var claims = await _profileService.GetClaimAsync(user);

            if (claims == null)
                throw new NullReferenceException("Claim is required for writing a comment");

            var claim = claims.FirstOrDefault();

            if (claim.Type != "Moderator" && claim.Type != "User")
            {
                throw new InvalidOperationException("You are not permited to write comment.");
            }

            var post = _unitOfWork.Posts.GetById(comment.PostId);

            if (post == null)
                throw new InvalidOperationException("No comment can be created without proper post");

            await _unitOfWork.Comments.AddAsync(new EO.Comment
            {
                CommentText = comment.CommentText,
                CreatorEmail = comment.CreatorEmail,
                CreatorId = comment.CreatorId,
                PostId = comment.PostId,
            });
            _unitOfWork.Save();
        }

        public IList<Comment> GetComments(Guid postId)
        {
            if (postId == Guid.Empty)
                throw new ArgumentNullException("Post id can not be empty.");

            var post = _unitOfWork.Posts.GetById(postId);

            if (post == null)
                return null;

            var comments = new List<Comment>();
            var commentEntities = _unitOfWork.Comments.Get(x => x.PostId == postId, "");

            foreach (var comment in commentEntities)
            {
                comments.Insert(0, new Comment
                {
                    CommentText = comment.CommentText,
                    CreatorEmail = comment.CreatorEmail,
                    CreatorId = comment.CreatorId,
                    Id = comment.Id,
                    PostId = comment.PostId
                });
            }

            return comments;
        }

        public Comment GetComment(Guid commentId)
        {
            if (commentId == Guid.Empty)
                throw new ArgumentNullException("Comment id can not be empty.");

            var comment = _unitOfWork.Comments.GetById(commentId);

            if (comment == null)
                return null;

            return new Comment
            {
                CommentText = comment.CommentText,
                CreatorEmail = comment.CreatorEmail,
                CreatorId = comment.CreatorId,
                Id = comment.Id,
                PostId = comment.PostId
            };
        }

        public async Task Update(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException("No comment provided");

            var user = await _profileService.GetUserByIdAsync(comment.CreatorId);

            if (user == null)
                throw new FileNotFoundException("User not found with the creator id");

            var claims = await _profileService.GetClaimAsync(user);

            if (claims == null)
                throw new NullReferenceException("Claim is required for updating a comment.");

            var claim = claims.FirstOrDefault();

            if (claim.Type != "Moderator" && claim.Type != "User")
            {
                throw new InvalidOperationException("You are not permited to update a comment.");
            }

            var commentEntity = _unitOfWork.Comments.GetById(comment.Id);

            if (commentEntity == null)
                throw new FileNotFoundException("The comment is not valid");

            if (commentEntity.CreatorId != comment.CreatorId)
                throw new InvalidOperationException("You are not cretor of the comment.");

            var post = _unitOfWork.Posts.GetById(comment.PostId);

            if (post == null)
                throw new InvalidOperationException("No comment post can be edited without proper post id");

            if (commentEntity.PostId != post.Id)
                throw new InvalidOperationException("Post not matched");


            commentEntity.CommentText= comment.CommentText;
            _unitOfWork.Save();
        }
    }
}