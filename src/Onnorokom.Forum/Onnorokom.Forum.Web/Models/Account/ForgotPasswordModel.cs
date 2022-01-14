using System.ComponentModel.DataAnnotations;

namespace Onnorokom.Forum.Web.Models.Account
{
    public class ForgotPasswordModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
