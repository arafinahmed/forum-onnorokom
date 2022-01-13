using Onnorokom.Forum.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.Forum.Membership.Services
{
    public interface IMailSenderService
    {
        Task SendEmailConfirmationMailAsync(ApplicationUser user, string verificationCode);
        Task SendResetPasswordMailAsync(ApplicationUser user, string verificationCode);
    }
}