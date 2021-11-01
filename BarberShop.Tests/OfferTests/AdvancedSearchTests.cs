using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberShop.BLL.Services;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.EF;
using BarberShop.DAL.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BarberShop.Tests.OfferTests
{
    public class AdvancedSearchTests
    {
        public DbContextOptions<ApplicationContext> Opt;

        public IEnumerable<Offer> GetTestOffers()
        {
            List<Offer> offers = new List<Offer>()
            {
                new Offer() { Id = 4, Title = "Offer 4", Cost = 10 },
                new Offer() { Id = 5, Title = "Offer 5", Cost = 20 },
                new Offer() { Id = 6, Title = "Offe 6", Cost = 30 },
                new Offer() { Id = 7, Title = "Offe 7", Cost = 40 },
                new Offer() { Id = 8, Title = "Offer 78", Cost = 40 },
            };

            return offers;
        }

        [SetUp]
        public void SetUp()
        {
            Opt = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var context = new ApplicationContext(Opt);
            // Remove seed data
            context.RemoveRange(context.Offers);
            context.SaveChanges();

            context.Offers.AddRange(GetTestOffers());
            context.SaveChanges();
        }

        [Test]
        public async Task TestEmptySubTitle()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            IEnumerable<Offer> offers = await repository.OfferRepository().GetAllAsync();

            OfferService offerService = new OfferService(repository);
            Assert.AreEqual(0, offerService.AdvancedSearch(new Offer{ Title = "Offers" }).Count());
        }

        [Test]
        public async Task TestEmptyCost()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            IEnumerable<Offer> offers = await repository.OfferRepository().GetAllAsync();

            OfferService offerService = new OfferService(repository);
            Assert.AreEqual(0, offerService.AdvancedSearch(new Offer { Cost = 9 }).Count());
        }

        [Test]
        public async Task TestSubTitle()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            IEnumerable<Offer> offers = await repository.OfferRepository().GetAllAsync();

            OfferService offerService = new OfferService(repository);
            Assert.AreEqual(3, offerService.AdvancedSearch(new Offer { Title = "Offer" }).Count());
        }

        [Test]
        public async Task TestCost()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            IEnumerable<Offer> offers = await repository.OfferRepository().GetAllAsync();

            OfferService offerService = new OfferService(repository);
            Assert.AreEqual(2, offerService.AdvancedSearch(new Offer { Cost = 40}).Count());
        }

        [Test]
        public async Task TestCostAndSubTitle()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            IEnumerable<Offer> offers = await repository.OfferRepository().GetAllAsync();

            OfferService offerService = new OfferService(repository);
            Assert.AreEqual(1, offerService.AdvancedSearch(new Offer { Title = "Offer", Cost = 40 }).Count());
        }
    }
}
