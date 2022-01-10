using Autofac;
using Onnorokom.Forum.Web.Models.Account;

namespace Onnorokom.Forum.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterModel>().AsSelf();

            base.Load(builder);
        }
    }
}
