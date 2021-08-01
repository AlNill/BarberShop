using BarberShop.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DAL.EF.Contexts
{
    public class BarberContext : DbContext
    {
        public DbSet<Barber> Barbers { get; set; }

        public BarberContext()
        {
            //Database.EnsureCreated();
        }

        public BarberContext(DbContextOptions<BarberContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=BarberShopDb;Trusted_Connection=True;"
                );
        }
    }
}
