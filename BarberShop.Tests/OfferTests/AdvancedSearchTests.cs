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
            OfferService offerService = new OfferService(repository);
            Assert.AreEqual(0, offerService.AdvancedSearch(subtitle:"Offers").Count());
        }

        [Test]
        public async Task TestEmptyCost()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            OfferService offerService = new OfferService(repository);
            Assert.AreEqual(0, offerService.AdvancedSearch(maxCost:9).Count());
        }

        [Test]
        public async Task TestSubTitle()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            OfferService offerService = new OfferService(repository);
            Assert.AreEqual(3, offerService.AdvancedSearch(subtitle:"Offer").Count());
        }

        [Test]
        public async Task TestCost()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            OfferService offerService = new OfferService(repository);
            Assert.AreEqual(2, offerService.AdvancedSearch(minCost:40).Count());
        }

        [Test]
        public async Task TestCostAndSubTitle()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            OfferService offerService = new OfferService(repository);
            Assert.AreEqual(1, offerService.AdvancedSearch(subtitle:"Offer", minCost:40).Count());
        }

        [Test]
        public async Task TestRangeCost()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            OfferService offerService = new OfferService(repository);
            Assert.AreEqual(2, offerService.AdvancedSearch(minCost: 5, maxCost:29).Count());
        }

        [Test]
        public async Task TestRangeCostAll()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            OfferService offerService = new OfferService(repository);
            Assert.AreEqual(5, offerService.AdvancedSearch(minCost: 5, maxCost: 50).Count());
        }

        [Test]
        public async Task TestMinMoreMaxException()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            OfferService offerService = new OfferService(repository);
            Assert.Throws<ArgumentException>(() => offerService.AdvancedSearch(minCost: 15, maxCost: 3));
        }

        [Test]
        public async Task TestNegativeMinException()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            OfferService offerService = new OfferService(repository);
            Assert.Throws<ArgumentException>(() => offerService.AdvancedSearch(minCost: -15, maxCost: 3));
        }

        [Test]
        public async Task TestNegativeMaxException()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            OfferService offerService = new OfferService(repository);
            Assert.Throws<ArgumentException>(() => offerService.AdvancedSearch(maxCost: -3));
        }

        [Test]
        public async Task TestNegativeMinAndMaxException()
        {
            await using var context = new ApplicationContext(Opt);
            var repository = new UnitOfWork(context);
            OfferService offerService = new OfferService(repository);
            Assert.Throws<ArgumentException>(() => offerService.AdvancedSearch(minCost:-4, maxCost: -3));
        }
    }
}
