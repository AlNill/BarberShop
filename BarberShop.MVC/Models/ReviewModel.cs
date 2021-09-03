using System.ComponentModel.DataAnnotations;

namespace BarberShop.MVC.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string UserReview { get; set; }
        [Required]
        public int BarberId { get; set; }
        [Required]
        public BarberModel Barber { get; set; }
    }
}
