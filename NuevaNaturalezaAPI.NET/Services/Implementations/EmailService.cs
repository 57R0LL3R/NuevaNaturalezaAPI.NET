using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class EmailService: IEmailService
    {
        private readonly string _fromEmail = "nuevanaturaleza2025@gmail.com";
        private readonly string _appPassword = "mpbs tgij nbct ayrl";
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.Credentials = new NetworkCredential(_fromEmail, _appPassword);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true 
                };

                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
