using Autofac;
using Onnorokom.Forum.Web.Models.Account;
using Onnorokom.Forum.Web.Models.Home;
using Onnorokom.Forum.Web.Models.Moderator;
using Onnorokom.Forum.Web.Models.Topic;

namespace Onnorokom.Forum.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<ConfirmEmailModel>().AsSelf();
            builder.RegisterType<CreateBoardModel>().AsSelf();
            builder.RegisterType<BoardListModel>().AsSelf();
            builder.RegisterType<EditBoardModel>().AsSelf();
            builder.RegisterType<DeleteBoardModel>().AsSelf();
            builder.RegisterType<LoadAllTopicsModel>().AsSelf();
            builder.RegisterType<CreateTopicModel>().AsSelf();
            builder.RegisterType<EditTopicModel>().AsSelf();
            builder.RegisterType<DeleteTopicModel>().AsSelf();

            base.Load(builder);
        }
    }
}
