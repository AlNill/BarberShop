namespace BarberShop.DAL.Common.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Cost { get; set; }
        public int Duration { get; set; } = 45;
    }
}
