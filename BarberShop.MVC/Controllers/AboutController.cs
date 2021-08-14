using Microsoft.AspNetCore.Mvc;

namespace BarberShop.MVC.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
