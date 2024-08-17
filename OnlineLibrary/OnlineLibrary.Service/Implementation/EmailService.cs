using OnlineLibrary.Domain.Helper;
using OnlineLibrary.Service.Interface;
using MailKit.Security;
using MimeKit;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OnlineLibrary.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings mailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            mailSettings = new MailSettings(
                configuration["EmailSettings:SmtpServer"],
                int.Parse(configuration["EmailSettings:SmtpServerPort"]),
                configuration["EmailSettings:EmailDisplayName"],
                configuration["EmailSettings:SendersName"],
                configuration["EmailSettings:SmtpUserName"],
                configuration["EmailSettings:SmtpPassword"],
                bool.Parse(configuration["EmailSettings:EnableSsl"])
                );

            _logger = logger;
        }

        public async Task SendEmailAsync(EmailMessage email)
        {
            var mailBoxAddress = new MailboxAddress(mailSettings.SendersName, mailSettings.SmtpUserName);

            var emailMessage = new MimeMessage
            {
                Sender = mailBoxAddress,
                Subject = email.Subject
            };

            emailMessage.From.Add(mailBoxAddress);
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = email.Content };
            emailMessage.To.Add(new MailboxAddress(email.MailTo, email.MailTo));

            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOptions = SecureSocketOptions.Auto;
                    await smtp.ConnectAsync(mailSettings.SmtpServer, mailSettings.SmtpServerPort, socketOptions);

                    if (!string.IsNullOrEmpty(mailSettings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(mailSettings.SmtpUserName, mailSettings.SmtpPassword);
                    }

                    await smtp.SendAsync(emailMessage);
                    await smtp.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
           
        }
    }
}
