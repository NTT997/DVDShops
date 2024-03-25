using DVDShops.Models;

namespace DVDShops.Services.MailService
{
    public interface IMailService
    {
        bool SendMail(User to, string subject, string content);
        //bool ReceiveMail(string from, string subject, string content);
    }
}
