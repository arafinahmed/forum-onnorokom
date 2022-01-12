using Autofac;
using Onnorokom.Forum.Web.Models.Account;
using Onnorokom.Forum.Web.Models.Moderator;

namespace Onnorokom.Forum.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<ConfirmEmailModel>().AsSelf();
            builder.RegisterType<CreateBoardModel>().AsSelf();

            base.Load(builder);
        }
    }
}
