using BarberShop.DAL.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DAL.EF.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BusyRecord> BusyRecords { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BarberShopDb;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "Admin";
            string userRoleName = "User";

            // Seed roles
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };

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

            User user = new User()
            {
                Id = 2,
                Name = "UserName",
                Surname = "UserSurname",
                FatherName = "UserFatherName",
                Password = "User",
                NickName = "User",
                PhoneNumber = "+111223333333",
                RoleId = 2,
            };

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

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { admin , user });
            modelBuilder.Entity<Barber>().HasData(new Barber[] {barber1, barber2, barber3 });
            base.OnModelCreating(modelBuilder);
        }
    }
}
