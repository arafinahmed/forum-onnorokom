using Autofac;

namespace Onnorokom.Forum.Web.Models.Account
{
    public class ConfirmEmailModel
    {
        public string? StatusMessage { get; set; }
        public bool IsSuccess { get; set; }

        public ConfirmEmailModel()
        {
        }
    }
}
