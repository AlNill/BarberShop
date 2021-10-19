using System.Collections.Generic;

namespace BarberShop.MVC.Models.UsersPage
{
    public class IndexModel
    {
        public IEnumerable<UserModel> Users { get; set; }
        public PageModel PageModel { get; set; }
    }
}
