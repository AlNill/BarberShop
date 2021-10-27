using System.Net;
using System.Net.Mail;
using BarberShop.BLL.Interfaces;

namespace BarberShop.BLL
{
    public class EmailNotificator: IEmailNotificator
    {
        private readonly string _fromEmailPass;
        private readonly string _fromEmailAddr;

        public EmailNotificator(string fromEmailAddr, string fromEmalPass)
        {
            _fromEmailAddr = fromEmailAddr;
            _fromEmailPass = fromEmalPass;
        }

        public void SmtpNotify(string title, string text, string toEmailAddr)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(_fromEmailAddr);
            message.To.Add(new MailAddress(toEmailAddr));
            message.Subject = title;
            message.IsBodyHtml = false;
            message.Body = text;
            smtp.Port = 587;
            smtp.Host = "smtp.yandex.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_fromEmailAddr, _fromEmailPass);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}
