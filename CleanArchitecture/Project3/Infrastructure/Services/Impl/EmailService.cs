using Infrastructure.Config;
using Infrastructure.Helpter;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Services.Impl
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting emailSetting;

        public EmailService(IOptions<EmailSetting> emailSettings)
        {
            this.emailSetting = emailSettings.Value;
        }

        public async Task SendEmailAsync(EmailRequest emailRequest)
        {
            try
            {
                using (var smtpClient = new SmtpClient(emailSetting.Host, emailSetting.Port))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(emailSetting.Email, emailSetting.Password);
                    smtpClient.EnableSsl = true; 

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(emailSetting.Email, emailSetting.Displayname),
                        Subject = emailRequest.Subject,
                        Body = emailRequest.Body,
                        IsBodyHtml = true 
                    };
                    mailMessage.To.Add(emailRequest.ToEmail);

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions accordingly, e.g., log them
                Console.WriteLine($"Failed to send email: {ex.Message}");
                throw; // Rethrow exception to be caught by the caller if necessary
            }
        }
    }
}
