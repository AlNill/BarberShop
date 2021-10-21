using BarberShop.DAL.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DAL.EF.Seed
{
    public static class UsersSeed
    {
        public static void SeedUsers(ModelBuilder modelBuilder)
        {
            // Seed users
            User admin = new User
            {
                Id = 1,
                Name = "Admin",
                Surname = "Admin",
                FatherName = "Admin",
                Password = "Admin",
                NickName = "Admin",
                PhoneNumber = "+111223333333",
                RoleId = 1,
            };


            User[] users = new User[21];
            users[0] = admin;
            for (int i = 1; i < 21; ++i)
            {
                users[i] = new User()
                {
                    Id = i + 1,
                    Name = $"Name{i}",
                    Surname = $"Surname{i}",
                    FatherName = $"FatherName{i}",
                    Password = $"User{i}",
                    NickName = $"User{i}",
                    PhoneNumber = "+111223333333",
                    RoleId = 2,
                };
            }

            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
