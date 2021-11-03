using System;
using System.Threading.Tasks;

namespace BarberShop.BLL.Interfaces
{
    public interface IHtmlTemplatePreparer
    {
        public Task<string> GetRenderedHtml(string nickName, string offer, DateTime dateTime, string masterName);
    }
}
