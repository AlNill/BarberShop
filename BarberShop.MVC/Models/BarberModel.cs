using System.ComponentModel.DataAnnotations;

namespace BarberShop.MVC.Models
{
    public class BarberModel: PersonModel
    {
        [Required]
        [StringLength(150)]
        public string Information { get; set; }

        public string ImagePath { get; set; } = "/images/standart_short.jpg";
    }
}
