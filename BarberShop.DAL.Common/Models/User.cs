namespace BarberShop.DAL.Common.Models
{
    public class User: Person
    {
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; } = 2;
        public Role Role { get; set; }
        public bool IsBanned { get; set; } = false;
    }
}
