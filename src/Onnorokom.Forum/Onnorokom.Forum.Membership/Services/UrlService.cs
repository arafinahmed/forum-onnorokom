using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Onnorokom.Forum.Membership.Services
{
    public class UrlService : IUrlService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly LinkGenerator _generator;

        public UrlService(IHttpContextAccessor accessor, LinkGenerator generator)
        {
            _accessor = accessor;
            _generator = generator;
        }

        public string GenerateAbsoluteUrl(string controller, string action, object parameters)
        {
            return _generator.GetUriByAction(_accessor.HttpContext, action, controller, parameters);
        }
    }
}
