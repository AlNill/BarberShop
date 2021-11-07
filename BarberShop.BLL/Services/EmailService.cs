using System;
using System.Threading.Tasks;
using BarberShop.BLL.Interfaces;

namespace BarberShop.BLL.Services
{
    public class EmailService : IEmailService
    {
        private string _title = "Black Rock event";
        private readonly IEmailNotificator _emailNotificator;
        private readonly IHtmlTemplatePreparer _htmlTemplatePreparer;

        public EmailService(IEmailNotificator emailNotificator, 
            IHtmlTemplatePreparer htmlTemplatePreparer)
        {
            _emailNotificator = emailNotificator;
            _htmlTemplatePreparer = htmlTemplatePreparer;
        }

        public async Task SmtpHtmlBodyYandexNotify(string nickName, string offer, 
            DateTime dateTime, string masterName, string toEmailAddr)
        {
            string renderedBody = await _htmlTemplatePreparer.GetRenderedHtml(nickName, offer, dateTime, masterName);
            _emailNotificator.SmtpHtmlBodyYandexNotify(_title, renderedBody, toEmailAddr);
        }

        public void SmtpYandexNotify(string text, string toEmailAddr)
        {
            _emailNotificator.SmtpYandexNotify(_title, text, toEmailAddr);
        }
    }
}
