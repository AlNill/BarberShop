using BarberShop.DAL.Common.Models;

namespace BarberShop.DAL.EF.Seed
{
    public static class BarbersSeed
    {
        public static Barber[] SeedBarbers()
        {
            // Seed barbers
            Barber barber1 = new Barber()
            {
                Id = 1,
                Name = "Joe",
                Surname = "God",
                FatherName = "Mark",
                Information = "Have hairstylist license and Organizational and " +
                              "time-management abilities."
            };

            Barber barber2 = new Barber()
            {
                Id = 2,
                Name = "Petr",
                Surname = "Shark",
                FatherName = "Aleksandrovich",
                Information = "High school diploma.\r\n" +
                              "Strong communication, listening, and interpersonal skills.",
            };

            Barber barber3 = new Barber()
            {
                Id = 3,
                Name = "Mark",
                Surname = "House",
                FatherName = "John",
                Information = "Have a GED certificate. \r\n" +
                              "Attention to detail.",
            };
            return new Barber[] {barber1, barber2, barber3};
        }
    }
}
