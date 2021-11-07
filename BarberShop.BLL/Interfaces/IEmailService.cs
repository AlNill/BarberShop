using System;
using System.Threading.Tasks;

namespace BarberShop.BLL.Interfaces
{
    public interface IEmailService
    {
        public Task SmtpHtmlBodyYandexNotify(string nickName, string offer,
            DateTime dateTime, string masterName, string toEmailAddr);

        public void SmtpYandexNotify(string text, string toEmailAddr);
    }
}
