using Demo.DAL.Entities.Identity;
using System.Net;
using System.Net.Mail;

namespace Demo.BLL.Common.Services.EmailSettings
{
    public class EmailSettings : IEmailSettings
    {
        public void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("", "");
            client.Send("", email.To, email.Subject, email.Body);

        }
    }
}
