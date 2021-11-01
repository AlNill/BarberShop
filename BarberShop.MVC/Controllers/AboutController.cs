﻿using BarberShop.MVC.Controllers.Base;
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
