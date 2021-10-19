using System;
using System.Collections.Generic;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Services
{
    public class OfferService: IServiceService
    {
        private readonly IGenericRepository<Offer> _repository;

        public OfferService(IGenericRepository<Offer> repository)
        {
            _repository = repository;
        }

        public Offer GetById(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Offer> GetServicesForSubTitle(string subTitle)
        {
            return _repository.Get(s => s.Title.Contains(subTitle));
        }

        public IEnumerable<Offer> AdvancedSearch(Offer offerParams)
        {
            Func<Offer, bool> predicate = (s => (
                (offerParams.Title == null || s.Title.Contains(offerParams.Title)) &&
                (offerParams.Cost == 0 || s.Cost == offerParams.Cost)
                ));
            return _repository.Get(predicate);
        }

        public IEnumerable<Offer> GetAll()
        {
            return _repository.GetAll();
        }

        public void Create(Offer offer)
        {
            _repository.Create(offer);
        }

        public void Update(Offer offer)
        {
            _repository.Update(offer);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
