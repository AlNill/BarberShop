namespace BarberShop.DAL.Common.Models
{
    public class User: Person
    {
        public string PhoneNumber { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
    }
}
