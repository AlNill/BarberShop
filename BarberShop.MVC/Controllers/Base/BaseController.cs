using System.Net;
using System.Security.Claims;
using BarberShop.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.MVC.Controllers.Base
{
    public class BaseController : Controller
    {
        internal ILoggerService Logger => HttpContext.RequestServices.GetService<ILoggerService>();

        internal string GetUserNickNameFromContext()
        {
            return HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
        }

        internal string GetUserRoleFromContext()
        {
            return HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
