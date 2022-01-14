using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onnorokom.Forum.Membership.Entities;
using Onnorokom.Forum.Web.Models.Comment;
using Onnorokom.Forum.Web.Models.Home;

namespace Onnorokom.Forum.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CommentController> _logger;
        private readonly ILifetimeScope _scope;

        public CommentController(
            UserManager<ApplicationUser> userManager,
            ILifetimeScope scope,
            ILogger<CommentController> logger)
        {
            _userManager = userManager;
            _logger = logger;
            _scope = scope;
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoadAllCommentsModel model)
        {
            try
            {
                var createCommentModel = _scope.Resolve<CreateCommentModel>();
                createCommentModel.CommentText = model.CommentText;
                createCommentModel.CreatorEmail = _userManager.GetUserName(User);
                createCommentModel.CreatorId = Guid.Parse(_userManager.GetUserId(User));
                createCommentModel.PostId = model.PostId;
                await createCommentModel.CreateComment(Guid.Parse(_userManager.GetUserId(User)));
                    
                return RedirectToAction("Comments", "Home", new { id = model.PostId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _logger.LogError(ex, "Post creation Failed");
                return RedirectToAction("Comments", "Home", new { id = model.PostId });
            }
        }
    }
}