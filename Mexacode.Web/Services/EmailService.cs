using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mexacode.Web.Models;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Mexacode.Web.Services
{
    public class EmailService : IEmailService
    {
        private ILogger<EmailService> _logger;
        private readonly AppSettings _appSettings;
        public EmailService(ILogger<EmailService> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }
        public async Task SendMailAsync(EmailModel email)
        {
            var emailMessage = new MimeMessage();
            var companyEmail = _appSettings.ContactUs.Email;
            var password = _appSettings.ContactUs.Password;

            emailMessage.From.Add(new MailboxAddress("", email.Email));
            emailMessage.To.Add(new MailboxAddress("", companyEmail));
            emailMessage.Subject = $"Asunto: {email.Asunto} -- De:{email.Empresa}-{email.Email}";
            emailMessage.Body = new TextPart("plain") { Text = email.Mensaje };
            _logger.LogInformation("Sending email: {@email}", email);
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                    await client.AuthenticateAsync(companyEmail, password);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Unhandled error sending email:{@email}, exception:{@ex}", email, ex);
                }
            }
            _logger.LogInformation("Email sent: {@email}", email);
        }
    }
}
