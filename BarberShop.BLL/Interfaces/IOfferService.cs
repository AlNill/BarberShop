using System.Collections.Generic;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Interfaces
{
    public interface IOfferService
    {
        Offer GetById(int id);
        IEnumerable<Offer> GetAll();
        void Create(Offer offer);
        void Update(Offer offer);
        void Delete(int id);
        public IEnumerable<Offer> GetServicesForSubTitle(string subTitle);
        public IEnumerable<Offer> AdvancedSearch(Offer offerParams);
    }
}
