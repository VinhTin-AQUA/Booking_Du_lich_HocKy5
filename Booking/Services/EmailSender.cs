using Booking.Interfaces;
using Booking.Models;
using Booking.Models.MailService;
using MailKit.Net.Smtp;
using MimeKit;

namespace Booking.Services
{
    public class EmailSender : IEmailSender
    {
        // inject dịch vụ EmailConfiguration để lấy cấu hình gửi mail
        private readonly EmailConfiguration emailConfig;
        private readonly IAuthenRepository authenRepository;
        private readonly IConfiguration configuration;

        public EmailSender(IAuthenRepository authenRepository, IConfiguration configuration, EmailConfiguration emailConfig)
        {
            this.authenRepository = authenRepository;
            this.configuration = configuration;
            this.emailConfig = emailConfig;
        }

        // tạo email (thư gửi đi)
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(emailConfig.DisplayName, emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
            return emailMessage;
        }

        public async Task<bool> SendEmail(Message email)
        {
            bool isSent = true;
            // tạo email
            var emailMessage = CreateEmailMessage(email);
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(emailConfig.SmtpServer, emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(emailConfig.UserName, emailConfig.Password);
                    await client.SendAsync(emailMessage);
                }
                catch
                {
                    isSent = false;
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
            return isSent;
        }


        public async Task<bool> SendEmailConfirmAsync(AppUser user, string action, string token)
        {
            string url = $"https://localhost:7231/authentication/{action}?token={token}&email={user.Email}";

            Message message = new Message(new string[] { user.Email! },
                "Confirm Email",
                $"<p>Chúng tôi rất vui khi bạn sử dụng ứng dụng của chúng tôi. Nhấn <a href='{url}'>đây</a> to để xác thực</p>");
            return await SendEmail(message);
        }
    }
}
