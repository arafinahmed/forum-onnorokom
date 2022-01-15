using Autofac;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Onnorokom.Forum.Web.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Provide a valid email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide a new password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please provide a confirmation password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]

        public string ConfirmPassword { get; set; }
        public string? ReturnUrl { get; set; }
        public IList<AuthenticationScheme>? ExternalLogins { get; set; }

        public RegisterModel()
        {
        }
    }
}
