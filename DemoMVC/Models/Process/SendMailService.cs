using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;


namespace DemoMVC.Models.Process
{
    public class SendMailService : IEmailSender
    {
        private readonly MailSettings mailSettings;
        private readonly ILogger<SendMailService> logger;
        public SendMailService(IOptions<MailSettings> _mailSettings, ILogger<SendMailService> _logger)
        {
            mailSettings = _mailSettings.Value;
            logger = _logger;
            logger.LogInformation("Create SendEmailService");
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(message);
            }
            catch (Exception ex)
            {
                //Gửi mail thất bại, nội dung mail sẽ lư vào thư mục mailSave
                System.IO.Directory.CreateDirectory("mailSave");
                var emailSaveFile = string.Format(@"mailSave/{0}.eml", Guid.NewGuid());
                await message.WriteToAsync(emailSaveFile);

                logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailSaveFile);
                logger.LogError(ex.Message);
            }
            smtp.Disconnect(true);
            logger.LogInformation("Send mail to: " + email);
        }
    }
}