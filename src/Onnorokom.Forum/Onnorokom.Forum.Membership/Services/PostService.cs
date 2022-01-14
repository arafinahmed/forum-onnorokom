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

        public Post GetPost(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("id can not be empty.");

            var postEntity = _unitOfWork.Posts.GetById(id);

            if (postEntity == null)
                return null;

            return new Post
            {
                Description = postEntity.Description,
                CreatorEmail = postEntity.CreatorEmail,
                CreatorId = postEntity.CreatorId,
                Id = postEntity.Id,
                TopicId = postEntity.TopicId
            };
        }

        public async Task UpdatePostDescription(Post post)
        {
            if (post == null)
                throw new ArgumentNullException("No post provided");

            var user = await _profileService.GetUserByIdAsync(post.CreatorId);

            if (user == null)
                throw new FileNotFoundException("User not found with the creator id");

            var claims = await _profileService.GetClaimAsync(user);

            if (claims == null)
                throw new NullReferenceException("Claim is required for updating a post");

            var claim = claims.FirstOrDefault();

            if (claim.Type != "Moderator" && claim.Type != "User")
            {
                throw new InvalidOperationException("You are not permited to update a post");
            }

            var postEntity = _unitOfWork.Posts.GetById(post.Id);

            if (postEntity == null)
                throw new FileNotFoundException("The post is not valid");

            if (postEntity.CreatorId != post.CreatorId)
                throw new InvalidOperationException("You are not cretor of the post.");

            var topic = _unitOfWork.Topics.GetById(post.TopicId);

            if (topic == null)
                throw new InvalidOperationException("No post can be edited without proper topic id");

            if (postEntity.TopicId != topic.Id)
                throw new InvalidOperationException("Topic not matched");


            postEntity.Description = post.Description;
            _unitOfWork.Save();
        }

        public async Task DeletePost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException("No post provided");

            var user = await _profileService.GetUserByIdAsync(post.CreatorId);

            if (user == null)
                throw new FileNotFoundException("User not found with the creator id");

            var claims = await _profileService.GetClaimAsync(user);

            if (claims == null)
                throw new NullReferenceException("Claim is required for deleting a post");

            var claim = claims.FirstOrDefault();

            if (claim.Type != "Moderator" && claim.Type != "User")
            {
                throw new InvalidOperationException("You are not permited to delete a post");
            }

            var postEntity = _unitOfWork.Posts.GetById(post.Id);

            if (postEntity == null)
                throw new FileNotFoundException("The post is not valid");

            if (postEntity.CreatorId != post.CreatorId)
                throw new InvalidOperationException("You are not cretor of the post.");

            var topic = _unitOfWork.Topics.GetById(post.TopicId);

            if (topic == null)
                throw new InvalidOperationException("No post can be deleted without proper topic id");

            if (postEntity.TopicId != topic.Id)
                throw new InvalidOperationException("Topic not matched");


            _unitOfWork.Posts.Remove(postEntity);
            _unitOfWork.Save();
        }

        public async Task<IList<Post>> GetPostByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException("User id can not be empty.");

            var user = await _profileService.GetUserAsync(userId);

            if (user == null)
                return null;

            var posts = new List<Post>();
            var postsEntity = _unitOfWork.Posts.Get(x => x.CreatorId == userId, "");

            foreach (var postEntity in postsEntity)
            {
                posts.Insert(0, new Post
                {
                    Description = postEntity.Description,
                    CreatorEmail = postEntity.CreatorEmail,
                    CreatorId = postEntity.CreatorId,
                    Id = postEntity.Id,
                    TopicId = postEntity.TopicId
                });
            }

            return posts;
        }
    }
}