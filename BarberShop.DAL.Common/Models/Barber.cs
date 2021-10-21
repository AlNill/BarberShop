namespace BarberShop.DAL.Common.Models
{
    public class Barber: Person
    {
        public string Information { get; set; }
        public string ImagePath { get; set; } = "/images/standart_short.jpg";
    }
}
