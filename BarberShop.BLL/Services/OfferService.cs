using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

        public async Task<Offer> Get(int id)
        {
            return await _repository.GetAsync(id);
        }

        public IEnumerable<Offer> GetServicesForSubTitle(string subTitle)
        {
            return _repository.Get(s => s.Title.Contains(subTitle));
        }

        public IEnumerable<Offer> AdvancedSearch(string subtitle = null, int minCost = 0, int maxCost = 0)
        {
            if ((minCost > maxCost && maxCost != 0) || minCost < 0 || maxCost < 0)
                throw new ArgumentException("Bad search parameters.");
            Func<Offer, bool> predicate = new Func<Offer, bool>(
                (offer) => (
                    (subtitle == null || offer.Title.Contains(subtitle)) &&
                    (minCost == 0 || offer.Cost >= minCost) &&
                    (maxCost == 0 || offer.Cost <= maxCost)
                    ));

            return _repository.Get(predicate);
        }

        public async Task<IEnumerable<Offer>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateAsync(Offer offer)
        {
            await _repository.CreateAsync(offer);
        }

        public async Task UpdateAsync(Offer offer)
        {
            await _repository.UpdateAsync(offer);
        }

        public async Task DeleteAsync(int id)
        {
            if(await _repository.ExistsAsync(id))
                await _repository.DeleteAsync(id);
        }
    }
}
