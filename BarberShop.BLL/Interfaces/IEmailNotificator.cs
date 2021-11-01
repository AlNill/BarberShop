namespace BarberShop.BLL.Interfaces
{
    public interface IEmailNotificator
    {
        public void SmtpNotify(string title, string text, string toEmailAddr);
    }
}
