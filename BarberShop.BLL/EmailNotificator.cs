using System.Net;
using System.Net.Mail;
using BarberShop.BLL.Interfaces;

namespace BarberShop.BLL
{
    public class EmailNotificator: IEmailNotificator
    {
        private readonly string _fromEmailPass;
        private readonly string _fromEmailAddr;

        public EmailNotificator(string fromEmailAddr, string fromEmailPass)
        {
            _fromEmailAddr = fromEmailAddr;
            _fromEmailPass = fromEmailPass;
        }

        private MailMessage SetMessageSettings(string title, string text, 
            string toEmailAddr, bool isHtmlBody=false)
        {
            MailMessage message = new MailMessage()
            {
                From = new MailAddress(_fromEmailAddr),
                Subject = title,
                IsBodyHtml = isHtmlBody,
                Body = text
            };
            message.To.Add(new MailAddress(toEmailAddr));
            return message;
        }

        private void SendSmtpYandexMsg(MailMessage message)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.yandex.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_fromEmailAddr, _fromEmailPass);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        public void SmtpYandexNotify(string title, string text, string toEmailAddr)
        {
            var message = SetMessageSettings(title, text, toEmailAddr);
            SendSmtpYandexMsg(message);
        }

        public void SmtpHtmlBodyYandexNotify(string title, string htmlBody, string toEmailAddr)
        {
            var message = SetMessageSettings(title, htmlBody, toEmailAddr, true);
            SendSmtpYandexMsg(message);
        }
    }
}
