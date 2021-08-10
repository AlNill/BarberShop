using System.ComponentModel.DataAnnotations;

namespace BarberShop.MVC.Models
{
    public class UserModel : PersonModel
    {
        [StringLength(30, ErrorMessage = "Nickname must be short than 30 symbols")]
        public string Nickname { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
