using BarberShop.DAL.Common.Models;

namespace BarberShop.DAL.EF.Seed
{
    public static class RolesSeed
    {
        public static Role[] SeedRoles()
        {
            string adminRoleName = "Admin";
            string userRoleName = "User";

            // Seed roles
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };

            return new Role[] {adminRole, userRole};
        }
    }
}
