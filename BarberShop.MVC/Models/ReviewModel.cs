using BarberShop.BLL.Models;

namespace BarberShop.MVC.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public string UserReview { get; set; }
        public int BarberId { get; set; }
        public BarberModel Barber { get; set; }
    }
}
