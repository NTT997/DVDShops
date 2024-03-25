using DVDShops.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace DVDShops.Services.MailService
{
    public class MailService : IMailService
    {
        private IConfiguration configuration;
        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool SendMail(User to, string subject, string content)
        {
            try
            {
                var host = configuration["EmailSettings:Smtp:Host"];
                var port = int.Parse(configuration["EmailSettings:Smtp:Port"]);
                var username = configuration["EmailSettings:Smtp:Username"];
                var password = configuration["EmailSettings:Smtp:Password"];
                var enable = bool.Parse(configuration["EmailSettings:Smtp:UseSsl"]);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("DVDSshop", username));
                message.To.Add(new MailboxAddress(to.UsersName, to.UsersEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = content;
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(host, port, enable);
                    client.Authenticate(username, password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
