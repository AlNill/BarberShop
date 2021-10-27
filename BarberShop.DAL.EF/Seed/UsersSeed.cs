using BarberShop.DAL.Common.Models;

namespace BarberShop.DAL.EF.Seed
{
    public static class UsersSeed
    {
        public static User[] SeedUsers()
        {
            User admin = new User
            {
                Id = 1,
                Name = "Admin",
                Surname = "Admin",
                FatherName = "Admin",
                Password = "Admin",
                NickName = "Admin",
                Email = "us3rt35t@yandex.ru",
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
                    Email = "us3rt35t@yandex.ru",
                    RoleId = 2,
                };
            }
            return users;
        }
    }
}
