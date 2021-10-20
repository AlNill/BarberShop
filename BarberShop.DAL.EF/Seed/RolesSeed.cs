using BarberShop.DAL.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DAL.EF.Seed
{
    public static class RolesSeed
    {
        public static void SeedRoles(ModelBuilder modelBuilder)
        {
            string adminRoleName = "Admin";
            string userRoleName = "User";

            // Seed roles
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
        }
    }
}
