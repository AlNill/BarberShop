using BarberShop.DAL.Common.Models;
using BarberShop.DAL.EF.Seed;
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
        public DbSet<Offer> Offers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RolesSeed.SeedRoles(modelBuilder);
            UsersSeed.SeedUsers(modelBuilder);
            BarbersSeed.SeedBarbers(modelBuilder);
            OffersSeed.SeedOffers(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
