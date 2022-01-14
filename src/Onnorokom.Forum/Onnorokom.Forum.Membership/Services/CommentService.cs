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
    }
}