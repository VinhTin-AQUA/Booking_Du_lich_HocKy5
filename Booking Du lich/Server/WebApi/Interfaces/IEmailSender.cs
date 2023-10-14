

using WebApi.Models;
using WebApi.Models.MailService;

namespace WebApi.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Message email);
        Task<bool> SendEmailConfirmAsync(ApplicationUser user, params string[] messages);
    }
}
