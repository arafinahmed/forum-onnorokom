using Autofac;
using Microsoft.AspNetCore.Mvc;
using Onnorokom.Forum.Web.Models;
using Onnorokom.Forum.Web.Models.Home;
using Onnorokom.Forum.Web.Models.Moderator;
using System.Diagnostics;

namespace Onnorokom.Forum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILifetimeScope _scope;

        public HomeController(ILogger<HomeController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Index()
        {
            var model = _scope.Resolve<BoardListModel>();
            model.LoadAllBoards();
            return View(model);
        }

        public IActionResult Topics(Guid id)
        {
            var model = _scope.Resolve<LoadAllTopicsModel>();
            model.LoadTopics(id);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Posts(Guid id)
        {
            var model = _scope.Resolve<LoadAllPostsModel>();
            model.Load(id);
            return View(model);
        }

        public IActionResult Comments(Guid id)
        {
            var model = _scope.Resolve<LoadAllCommentsModel>();
            model.Load(id);
            return View(model);
        }

        public async Task<IActionResult> User(Guid id)
        {
            var model = _scope.Resolve<LoadUserPosts>();
            try
            {
                await model.Load(id);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No user for showing post.");
                return View(model);
            }
        }
    }
}