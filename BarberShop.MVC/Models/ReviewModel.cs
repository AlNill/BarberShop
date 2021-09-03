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
        public BarberModel Barber { get; set; }
        [Required]
        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}