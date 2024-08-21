using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DotnetBackend.Services
{
    public class EmailSenderService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(IConfiguration configuration, ILogger<EmailSenderService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendSimpleEmailAsync(string toEmail, string body, string subject)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Your Name", "satyadutt2580@gmail.com")); // Set your name and email
                emailMessage.To.Add(new MailboxAddress("", toEmail));
                emailMessage.Subject = subject;

                var textPart = new TextPart(TextFormat.Plain)
                {
                    Text = body
                };

                var multipart = new Multipart("mixed");
                multipart.Add(textPart);

                emailMessage.Body = multipart;

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"],
                        int.Parse(_configuration["EmailSettings:SmtpPort"]), SecureSocketOptions.StartTls);

                    await client.AuthenticateAsync(_configuration["EmailSettings:SmtpUsername"],
                        _configuration["EmailSettings:SmtpPassword"]);

                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }

                _logger.LogInformation("Email sent successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email: {ex.Message}");
                throw;
            }
        }

        public async Task SendEmailWithAttachmentAsync(string toEmail, string body, string subject, string attachment)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Your Name", "satyadutt2580@gmail.com")); // Set your name and email
                emailMessage.To.Add(new MailboxAddress("", toEmail));
                emailMessage.Subject = subject;

                var textPart = new TextPart(TextFormat.Plain)
                {
                    Text = body
                };

                var attachmentPart = new MimePart("application", "octet-stream")
                {
                    Content = new MimeContent(File.OpenRead(attachment), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(attachment)
                };

                var multipart = new Multipart("mixed");
                multipart.Add(textPart);
                multipart.Add(attachmentPart);

                emailMessage.Body = multipart;

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"],
                        int.Parse(_configuration["EmailSettings:SmtpPort"]), SecureSocketOptions.StartTls);

                    await client.AuthenticateAsync(_configuration["EmailSettings:SmtpUsername"],
                        _configuration["EmailSettings:SmtpPassword"]);

                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }

                _logger.LogInformation("Email with attachment sent successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email with attachment: {ex.Message}");
                throw;
            }
        }
    }
}
