using Autofac;
using Onnorokom.Forum.Membership.Services;
using BO = Onnorokom.Forum.Membership.BusinessObject;

namespace Onnorokom.Forum.Web.Models.Home
{
    public class LoadAllTopicsModel
    {
        public Guid Id { get; set; }
        public string BoardName { get; set; }
        public IList<BO.Topic> Topics { get; set;}
        private ILifetimeScope _scope;
        private IBoardService _boardService;
        private ITopicService _topicService;
        public LoadAllTopicsModel() { }

        public LoadAllTopicsModel(IBoardService boardService, ITopicService topicService)
        {
            _boardService = boardService;
            _topicService = topicService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _boardService = _scope.Resolve<IBoardService>();
            _topicService = _scope.Resolve<ITopicService>();
        }

        public void LoadTopics(Guid boardId)
        {
            var board = _boardService.GetBoard(boardId);

            if (board == null)
            {
                BoardName = "No board found.";
                return;
            }
            Id = board.Id;
            BoardName = board.BoardName;

            Topics = _topicService.GetAllTopics(boardId);
        }
    }
}
