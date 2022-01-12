using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onnorokom.Forum.Membership.Entities;
using Onnorokom.Forum.Web.Models.Topic;

namespace Onnorokom.Forum.Web.Controllers
{
    [Authorize(Policy = "CommonPermission")]
    public class TopicController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TopicController> _logger;
        private readonly ILifetimeScope _scope;

        public TopicController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILifetimeScope scope,
            ILogger<TopicController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _scope = scope;
        }
        public IActionResult CreateTopic(Guid id)
        {
            var model = _scope.Resolve<CreateTopicModel>();
            model.LoadBoard(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic(CreateTopicModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    await model.CreateTopic(_userManager.GetUserName(User));
                    return RedirectToAction("Topics", "Home", model.BoardId);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.LogError(ex, "Board Creation Failed");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
