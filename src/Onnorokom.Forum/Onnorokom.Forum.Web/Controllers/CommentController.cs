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

        public async Task<IActionResult> Edit(Guid id)
        {
            var model = _scope.Resolve<EditCommentModel>();
            var userId = Guid.Parse(_userManager.GetUserId(User));
            try
            {
                await model.Load(id, userId);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _logger.LogError(ex, "Not permitted.");
                return View(model);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCommentModel model)
        {
            model.CreatorEmail = _userManager.GetUserName(User);
            model.CreatorId = Guid.Parse(_userManager.GetUserId(User));
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    await model.Edit(Guid.Parse(_userManager.GetUserId(User)));
                    return RedirectToAction("Comments", "Home", new { id = model.PostId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.LogError(ex, "Post update Failed");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}