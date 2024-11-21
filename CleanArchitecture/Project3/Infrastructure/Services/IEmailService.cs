using Infrastructure.Helpter;

namespace Infrastructure.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest emailRequest);
    }
}
