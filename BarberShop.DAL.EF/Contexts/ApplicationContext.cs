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

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BarberShopDb;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
