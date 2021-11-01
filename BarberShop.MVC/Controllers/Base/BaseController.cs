using BarberShop.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.MVC.Controllers.Base
{
    public class BaseController : Controller
    {
        internal ILoggerService Logger => HttpContext.RequestServices.GetService<ILoggerService>();
    }
}
