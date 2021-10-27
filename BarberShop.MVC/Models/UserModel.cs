using System.ComponentModel.DataAnnotations;

namespace BarberShop.MVC.Models
{
    public class UserModel : PersonModel
    {
        [StringLength(30, ErrorMessage = "Nickname must be short than 30 symbols")]
        public string NickName { get; set; }
        [Required]
        [Phone]
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; } = 2;
        public RoleModel Role { get; set; }
    }
}
