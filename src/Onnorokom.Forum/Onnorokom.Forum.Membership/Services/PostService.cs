using Onnorokom.Forum.Membership.BusinessObject;
using Onnorokom.Forum.Membership.UnitOfWorks;
using EO = Onnorokom.Forum.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.Services
{
    public class PostService : IPostService
    {
        private readonly IMembershipUnitOfWork _unitOfWork;
        private readonly IProfileService _profileService;

        public PostService(IMembershipUnitOfWork unitOfWork, IProfileService profileService)
        {
            _unitOfWork = unitOfWork;
            _profileService = profileService;
        }

        public async Task CreatePost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException("No post provided");

            var user = await _profileService.GetUserByIdAsync(post.CreatorId);

            if (user == null)
                throw new FileNotFoundException("User not found with the user id");

            if (user.Email != post.CreatorEmail)
                throw new InvalidOperationException("Post email not match with user email");

            var claims = await _profileService.GetClaimAsync(user);
            if (claims == null)
                throw new NullReferenceException("Claim is required for creating a topic");

            var claim = claims.FirstOrDefault();

            if (claim.Type != "Moderator" && claim.Type != "User")
            {
                throw new InvalidOperationException("You are not permited to create a Board");
            }
            var topic = _unitOfWork.Topics.GetById(post.TopicId);
            if (topic == null)
                throw new InvalidOperationException("No post can be created without proper topic");


            await _unitOfWork.Posts.AddAsync(new EO.Post 
                {
                    Description = post.Description,
                    CreatorEmail = post.CreatorEmail,
                    CreatorId = post.CreatorId,
                    TopicId = post.TopicId,
                }
            );
            _unitOfWork.Save();
        }

        public IList<Post> GetAllPosts(Guid topicId)
        {
            if (topicId == Guid.Empty)
                throw new ArgumentNullException("Topic id can not be empty.");

            var topic = _unitOfWork.Topics.GetById(topicId);
            if (topic == null)
                return null;

            var posts = new List<Post>();
            var postsEntity = _unitOfWork.Posts.Get(x => x.TopicId == topicId, "");

            foreach (var postEntity in postsEntity)
            {
                posts.Insert(0, new Post 
                {
                    Description = postEntity.Description,
                    CreatorEmail= postEntity.CreatorEmail,
                    CreatorId= postEntity.CreatorId,
                    Id = postEntity.Id,
                    TopicId = postEntity.TopicId
                });
            }
            return posts;
        }
    }
}
