using System;
using BusinessLayer;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Apit.Service
{
    public class MailService
    {
        private readonly ILogger<MailService> _logger;
        private readonly ProjectConfig.MailboxConfig _config;

        public MailService(ILogger<MailService> logger, ProjectConfig config)
        {
            _logger = logger;
            _config = config.Mailbox;
        }


        /// <summary>
        /// Send email via default configured SMTP-client
        /// </summary>
        /// <param name="recipient">User email address</param>
        /// <param name="subject">Mail title</param>
        /// <param name="body">Mail content (supports HTML)</param>
        public void SendEmail(string recipient, string subject, string body)
        {
            try
            {
                var message = new MimeMessage
                {
                    Subject = subject,
                    Body = new BodyBuilder
                    {
                        HtmlBody = "<div style=\"color: green;\"><a href=\"" + body + "\">Press ME!</a></div>"
                    }.ToMessageBody()
                };

                message.From.Add(new MailboxAddress(_config.AddressName, _config.AddressEmail));
                message.To.Add(MailboxAddress.Parse(recipient));

                using var client = new SmtpClient();

                // use port 465 or 587
                client.Connect(_config.ServiceHost, _config.ServicePort, true);
                client.Authenticate(_config.RealEmail, _config.RealEmailPassword);
                client.Send(message);

                client.Disconnect(true);
                _logger.LogInformation("Email sent to recipient: " + recipient);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
            }
        }
    }
}