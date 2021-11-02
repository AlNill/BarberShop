using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IOfferService
    {
        public Task<Offer> Get(int id);
        public Task<IEnumerable<Offer>> GetAllAsync();
        public Task CreateAsync(Offer offer);
        public Task UpdateAsync(Offer offer);
        public Task DeleteAsync(int id);
        public IEnumerable<Offer> GetServicesForSubTitle(string subTitle);
        public IEnumerable<Offer> AdvancedSearch(Offer offerParams);
    }
}
