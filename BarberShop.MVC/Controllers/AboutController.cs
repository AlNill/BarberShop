using BarberShop.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.MVC.Controllers
{
    [AllowAnonymous]
    public class AboutController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
