using System.Runtime.InteropServices.ComTypes;

namespace BarberShop.BLL.Models
{
    public abstract class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
    }
}
