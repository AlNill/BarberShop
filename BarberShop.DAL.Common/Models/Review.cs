namespace BarberShop.DAL.Common.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string UserReview { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BarberId { get; set; }
        public Barber Barber { get; set; }
    }
}
