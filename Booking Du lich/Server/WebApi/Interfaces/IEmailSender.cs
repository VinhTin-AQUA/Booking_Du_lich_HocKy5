

using WebApi.Models.MailService;

namespace WebApi.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Message email);
    }
}
