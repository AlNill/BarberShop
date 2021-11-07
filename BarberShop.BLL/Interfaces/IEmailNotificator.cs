namespace BarberShop.BLL.Interfaces
{
    public interface IEmailNotificator
    {
        public void SmtpYandexNotify(string title, string text, string toEmailAddr);
        public void SmtpHtmlBodyYandexNotify(string title, string htmlBody, string toEmailAddr);
    }
}
