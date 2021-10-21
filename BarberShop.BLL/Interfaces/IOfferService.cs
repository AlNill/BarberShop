using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IOfferService
    {
        public Task<Offer> GetById(int id);
        public Task<IEnumerable<Offer>> GetAll();
        public void Create(Offer offer);
        public void Update(Offer offer);
        public void Delete(int id);
        public IEnumerable<Offer> GetServicesForSubTitle(string subTitle);
        public IEnumerable<Offer> AdvancedSearch(Offer offerParams);
    }
}
