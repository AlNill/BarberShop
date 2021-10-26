using BarberShop.DAL.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DAL.EF.Seed
{
    public static class OffersSeed
    {
        public static Offer[] SeedOffers()
        {
            // Seed services
            Offer service1 = new Offer() { Id = 1, Title = "Mans haircut", Cost = 35 };
            Offer service2 = new Offer() { Id = 2, Title = "Child haircut", Cost = 30 };
            Offer service3 = new Offer() { Id = 3, Title = "Bread trim", Cost = 30 };

            return new Offer[] { service1, service2, service3 };
        }
    }
}
