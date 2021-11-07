using System;
using System.IO;
using System.Threading.Tasks;
using BarberShop.BLL.Interfaces;

namespace BarberShop.BLL
{
    public class HtmlTemplatePreparer: IHtmlTemplatePreparer
    {
        private readonly string _htmlTemplatePath;
        private string _nickNameSignature = "[\"NickName\"]";
        private string _offerSignature = "[\"Offer\"]";
        private string _dateTimeSignature = "[\"DateTime\"]";
        private string _masterNameSignature = "[\"Master\"]";


        public HtmlTemplatePreparer(string htmlTemplatePath)
        {
            _htmlTemplatePath = htmlTemplatePath;
        }

        private async Task<string> LoadHtmlTemplate()
        {
            using StreamReader sr = new StreamReader(_htmlTemplatePath);
            return await sr.ReadToEndAsync(); ;
        }

        public async Task<string> GetRenderedHtml(string nickName, string offer, DateTime dateTime, string masterName)
        {
            var htmlTemplate = await LoadHtmlTemplate();
            return htmlTemplate.Replace(_nickNameSignature, nickName).
                Replace(_offerSignature, offer).
                Replace(_dateTimeSignature, dateTime.ToString()).
                Replace(_masterNameSignature, masterName);
        }
    }
}
