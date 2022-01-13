using Autofac;
using Onnorokom.Forum.Web.Models.Account;
using Onnorokom.Forum.Web.Models.Home;
using Onnorokom.Forum.Web.Models.Moderator;
using Onnorokom.Forum.Web.Models.Post;
using Onnorokom.Forum.Web.Models.Topic;

namespace Onnorokom.Forum.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ConfirmEmailModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CreateBoardModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BoardListModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<EditBoardModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DeleteBoardModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<LoadAllTopicsModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CreateTopicModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<EditTopicModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DeleteTopicModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CreatePostModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<LoadAllPostsModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<EditPostModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DeletePostModel>().AsSelf().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}