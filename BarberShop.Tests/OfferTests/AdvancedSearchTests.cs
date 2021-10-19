using System;
using System.Collections.Generic;
using System.Linq;
using BarberShop.BLL.Services;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.EF.Contexts;
using BarberShop.DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
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
        public void TestEmptySubTitle()
        {
            using var context = new ApplicationContext(Opt);
            var repository = new GenericRepository<Offer>(context);
            IEnumerable<Offer> offers = repository.GetAll();

            OfferService offerService = new OfferOfferService(repository);
            Assert.AreEqual(0, offerService.AdvancedSearch(new Offer{ Title = "Offers" }).Count());
        }

        [Test]
        public void TestEmptyCost()
        {
            using var context = new ApplicationContext(Opt);
            var repository = new GenericRepository<Offer>(context);
            IEnumerable<Offer> offers = repository.GetAll();

            OfferService offerService = new OfferOfferService(repository);
            Assert.AreEqual(0, offerService.AdvancedSearch(new Offer { Cost = 9 }).Count());
        }

        [Test]
        public void TestSubTitle()
        {
            using var context = new ApplicationContext(Opt);
            var repository = new GenericRepository<Offer>(context);
            IEnumerable<Offer> offers = repository.GetAll();

            OfferService offerService = new OfferOfferService(repository);
            Assert.AreEqual(3, offerService.AdvancedSearch(new Offer { Title = "Offer" }).Count());
        }

        [Test]
        public void TestCost()
        {
            using var context = new ApplicationContext(Opt);
            var repository = new GenericRepository<Offer>(context);
            IEnumerable<Offer> offers = repository.GetAll();

            OfferService offerService = new OfferOfferService(repository);
            Assert.AreEqual(2, offerService.AdvancedSearch(new Offer { Cost = 40}).Count());
        }

        [Test]
        public void TestCostAndSubTitle()
        {
            using var context = new ApplicationContext(Opt);
            var repository = new GenericRepository<Offer>(context);
            IEnumerable<Offer> offers = repository.GetAll();

            OfferService offerService = new OfferOfferService(repository);
            Assert.AreEqual(1, offerService.AdvancedSearch(new Offer { Title = "Offer", Cost = 40 }).Count());
        }
    }
}
