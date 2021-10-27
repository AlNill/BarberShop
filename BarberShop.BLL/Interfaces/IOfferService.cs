using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IOfferService
    {
        public Task<Offer> GetById(int id);
        public Task<IEnumerable<Offer>> GetAll();
        public Task Create(Offer offer);
        public Task Update(Offer offer);
        public Task Delete(int id);
        public IEnumerable<Offer> GetServicesForSubTitle(string subTitle);
        public IEnumerable<Offer> AdvancedSearch(Offer offerParams);
    }
}
