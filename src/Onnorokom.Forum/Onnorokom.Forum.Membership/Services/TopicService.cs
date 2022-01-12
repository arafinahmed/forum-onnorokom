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
    public class TopicService : ITopicService
    {
        private readonly IMembershipUnitOfWork _unitOfWork;
        private readonly IProfileService _profileService;

        public TopicService(IMembershipUnitOfWork unitOfWork, IProfileService profileService)
        {
            _unitOfWork = unitOfWork;
            _profileService = profileService;
        }

        public async Task CreateTopic(Topic topic, Guid userId)
        {
            var user = await _profileService.GetUserByIdAsync(userId);

            if (user == null)
                throw new FileNotFoundException("User not found with the user id");

            var claims = await _profileService.GetClaimAsync(user);
            if (claims == null)
                throw new NullReferenceException("Claim is required for creating a topic");

            var claim = claims.FirstOrDefault();

            if (claim.Type != "Moderator" && claim.Type != "User")
            {
                throw new InvalidOperationException("You are not permited to create a Board");
            }
            var board = _unitOfWork.Boards.GetById(topic.BoardId);
            if (board == null)
                throw new InvalidOperationException("No topic can be created without proper boardId");

            var topics = _unitOfWork.Topics.Get(x => x.TopicName == topic.TopicName && x.BoardId == topic.BoardId, "");

            if (topics.Count > 0)
                throw new InvalidOperationException("Topic Name Already exists.");

            await _unitOfWork.Topics.AddAsync(new EO.Topic { TopicName = topic.TopicName, BoardId = topic.BoardId});
            _unitOfWork.Save();
        }
    }
}
