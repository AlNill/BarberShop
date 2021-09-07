using Microsoft.AspNetCore.Mvc;
using BarberShop.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected ILoggerService Logger => HttpContext.RequestServices.GetService<ILoggerService>();
    }
}
