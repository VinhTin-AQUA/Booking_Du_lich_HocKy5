
using MailKit.Net.Smtp;
using MimeKit;
using WebApi.Interfaces;
using WebApi.Models.MailService;

namespace WebApi.Services
{
    public class EmailSender : IEmailSender
    {
        // inject dịch vụ EmailConfiguration để lấy cấu hình gửi mail
        private readonly EmailConfiguration emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
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
    }
}
