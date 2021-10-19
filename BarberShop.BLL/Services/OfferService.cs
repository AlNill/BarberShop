using System;
using System.Collections.Generic;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.Common.Repositories;

namespace BarberShop.BLL.Services
{
    public class OfferService: IOfferService
    {
        private readonly IOfferRepository _repository;

        public OfferService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.OfferRepository();
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
            return _repository.AdvancedSearch(offerParams);
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
