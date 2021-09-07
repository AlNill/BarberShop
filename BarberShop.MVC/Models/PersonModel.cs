using System.ComponentModel.DataAnnotations;

namespace BarberShop.MVC.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Surname { get; set; }
        [Required]
        [StringLength(30)]
        public string FatherName { get; set; }
    }
}
