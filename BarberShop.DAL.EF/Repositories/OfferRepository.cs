using System;
using System.Collections.Generic;
using System.Linq;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.Common.Repositories;
using BarberShop.DAL.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DAL.EF.Repositories
{
    public class OfferRepository: GenericRepository<Offer>, IOfferRepository
    {
        public OfferRepository(ApplicationContext context) : base(context) { }

        public IEnumerable<Offer> AdvancedSearch(Offer offerParams)
        {
            Func<Offer, bool> predicate = (s => (
                (offerParams.Title == null || s.Title.Contains(offerParams.Title)) &&
                (offerParams.Cost == 0 || s.Cost == offerParams.Cost)
            ));
            return DbSet.AsNoTracking().AsEnumerable().Where(predicate).ToList();
        }
    }
}
