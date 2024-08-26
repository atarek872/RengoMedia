using Rengo.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Rengo.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var fromAddress = _configuration["EmailSettings:FromAddress"];
            var smtpHost = _configuration["EmailSettings:SmtpHost"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var smtpUser = _configuration["EmailSettings:SmtpUser"];
            var smtpPass = _configuration["EmailSettings:SmtpPass"];

            var message = new MailMessage(fromAddress, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.Credentials = new NetworkCredential(smtpUser, smtpPass);
                client.EnableSsl = true;
                await client.SendMailAsync(message);
            }
        }
    }
}
