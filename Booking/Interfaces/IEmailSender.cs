using Booking.Models;
using Booking.Models.MailService;

namespace Booking.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Message email);
        Task<bool> SendEmailConfirmAsync(AppUser user, string action, string token);
    }
}
