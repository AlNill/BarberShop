using BarberShop.DAL.Common.Models;

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
            Offer service4 = new Offer() { Id = 4, Title = "Clippers only", Cost = 15 };
            Offer service5 = new Offer() { Id = 5, Title = "Skin Fade with Haircut", Cost = 50 };
            Offer service6 = new Offer() { Id = 6, Title = "Head Shave", Cost = 40 };

            return new Offer[] { service1, service2, service3, service4, service5, service6 };
        }
    }
}
