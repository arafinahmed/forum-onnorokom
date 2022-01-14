using DevSkill.Http.Emails.Services;
using Onnorokom.Forum.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.Services
{
    public class MailSenderService : IMailSenderService
    {
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly IUrlService _urlService;
        private const string confirmationEmailSubject = "Confirmation Email";
        private const string resetPasswordEmailSubject = "Reset Password Email";

        public MailSenderService(IQueuedEmailService queuedEmailService,
            IUrlService urlService)
        {
            _queuedEmailService = queuedEmailService;
            _urlService = urlService;
        }

        public void Dispose()
        {

        }

        public async Task SendEmailConfirmationMailAsync(ApplicationUser user, string verificationCode)
        {
            var verificationLink = _urlService.GenerateAbsoluteUrl("Account", "ConfirmEmail",
                new { username = user.UserName, code = verificationCode, area = "" });

            var emailBody = $"Thanks for registering for an account on DevTeam!" +
                $" Before we get started, we just need to confirm that this is you." +
                $" <a href='{verificationLink}'>Click Here</a> to verify your email address ";

            await _queuedEmailService.SendSingleEmailAsync(user.UserName, user.UserName, confirmationEmailSubject, emailBody);
        }

        public async Task SendResetPasswordMailAsync(ApplicationUser user, string verificationCode)
        {
            var verificationLink = _urlService.GenerateAbsoluteUrl("Account", "ResetPassword",
                new { username = user.UserName, email = user.Email, code = verificationCode, area = "" });

            var emailBody = $"To reset password please" + $" <a href='{verificationLink}'>Click Here</a>";

            await _queuedEmailService.SendSingleEmailAsync(user.UserName, user.Email, resetPasswordEmailSubject, emailBody);
        }
    }
}