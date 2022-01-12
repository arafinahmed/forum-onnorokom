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
    public class BoardService : IBoardService
    {
        private readonly IMembershipUnitOfWork _unitOfWork;
        private readonly IProfileService _profileService;

        public BoardService(IMembershipUnitOfWork unitOfWork, IProfileService profileService)
        {
            _unitOfWork = unitOfWork;
            _profileService = profileService;
        }

        public async Task CreateBoard(Board board, Guid modId)
        {
            var user = await _profileService.GetUserByIdAsync(modId);

            if (user == null)
                throw new FileNotFoundException("User not found with the modId");

            var claims = await _profileService.GetClaimAsync(user);
            if (claims == null)
                throw new NullReferenceException("Claim is required for creating a board");

            var claim = claims.FirstOrDefault();

            if(claim.Type != "Moderator")
            {
                throw new InvalidOperationException("You are not permited to create a Board");
            }

            await _unitOfWork.Boards.AddAsync(new EO.Board { BoardName = board.BoardName});
        }
    }
}
