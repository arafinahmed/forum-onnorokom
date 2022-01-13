using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onnorokom.Forum.Membership.Entities;
using Onnorokom.Forum.Web.Models.Post;

namespace Onnorokom.Forum.Web.Controllers
{
    [Authorize(Policy = "CommonPermission")]
    public class PostController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<PostController> _logger;
        private readonly ILifetimeScope _scope;

        public PostController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILifetimeScope scope,
            ILogger<PostController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Create(Guid id)
        {
            var model = _scope.Resolve<CreatePostModel>();
            model.Load(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostModel model)
        {
            model.CreatorEmail = _userManager.GetUserName(User);
            model.CreatorId = Guid.Parse(_userManager.GetUserId(User));
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    await model.CreatePost();
                    return RedirectToAction("Posts", "Home", new {id = model.TopicId});
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.LogError(ex, "Post creation Failed");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
