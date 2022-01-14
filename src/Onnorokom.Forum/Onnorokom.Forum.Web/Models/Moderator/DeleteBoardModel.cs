using Autofac;
using Onnorokom.Forum.Membership.BusinessObject;
using Onnorokom.Forum.Membership.Services;
using System.ComponentModel.DataAnnotations;

namespace Onnorokom.Forum.Web.Models.Moderator
{
    public class DeleteBoardModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(256, ErrorMessage = "Board Name cannot exceed 255 characters.")]
        public string BoardName { get; set; }
        private ILifetimeScope _scope;
        private IBoardService _boardService;
        private IProfileService _profileService;

        public DeleteBoardModel() { }

        public DeleteBoardModel(IBoardService boardService, IProfileService profileService)
        {
            _boardService = boardService;
            _profileService = profileService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _boardService = _scope.Resolve<IBoardService>();
            _profileService = _scope.Resolve<IProfileService>();
        }

        public void LoadBoard(Guid id)
        {
            var board = _boardService.GetBoard(id);
            Id = board.Id;
            BoardName = board.BoardName;
        }

        internal async Task DeleteBoard(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
                throw new InvalidOperationException("User Email Must Be Provided For creating a Board");

            var user = await _profileService.GetUserAsync(userEmail);

            if (user == null)
                throw new FileNotFoundException("User not found with the email id");

            var claims = await _profileService.GetClaimAsync(user);

            if (claims == null)
                throw new NullReferenceException("Claim is required for creating a board");

            var claim = claims.FirstOrDefault();

            if (claim.Type != "Moderator")
            {
                throw new InvalidOperationException("You are not permited to create a Board");
            }

            await _boardService.DeleteBoard(new Board { BoardName = BoardName, Id = Id }, user.Id);
        }
    }
}