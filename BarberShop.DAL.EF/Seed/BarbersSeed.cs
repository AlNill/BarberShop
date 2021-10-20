using BarberShop.DAL.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DAL.EF.Seed
{
    public static class BarbersSeed
    {
        public static void SeedBarbers(ModelBuilder modelBuilder)
        {
            // Seed barbers
            Barber barber1 = new Barber()
            {
                Id = 1,
                Name = "Ivan",
                Surname = "Ivanov",
                FatherName = "Ivanovich",
                Information = "Cool information about barber Ivanov must be here.",
            };

            Barber barber2 = new Barber()
            {
                Id = 2,
                Name = "Petr",
                Surname = "Petrov",
                FatherName = "Petrovich",
                Information = "Cool information about barber Petrov must be here.",
            };

            Barber barber3 = new Barber()
            {
                Id = 3,
                Name = "Pup",
                Surname = "Pupkin",
                FatherName = "Pupkinovich",
                Information = "Cool information about barber Pupkin must be here.",
            };
            modelBuilder.Entity<Barber>().HasData(new Barber[] { barber1, barber2, barber3 });
        }
    }
}
