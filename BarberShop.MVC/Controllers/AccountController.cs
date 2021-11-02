using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Controllers.Base;
using BarberShop.MVC.Filters;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BarberShop.MVC.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ExceptionFilter]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                UserModel user = _mapper.Map<User, UserModel>(_userService.Get(
                    u => u.NickName == loginModel.NickName && u.Password == loginModel.Password));
                if (user != null)
                {
                    user = _mapper.Map<User, UserModel>(_userService.GetWithInclude(user.Id));
                    await Authenticate(user);
                    Logger.LogInformation($"Success login user {loginModel.NickName}");
                    return RedirectToAction("Index", "BusyRecords");
                }
                ModelState.AddModelError("", "Bad login or password");
            }
            Logger.LogInformation($"Login model validation error for user {loginModel.NickName}");
            return View(loginModel);
        }

        private async Task Authenticate(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.NickName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            Logger.LogInformation("Get request for register user");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ExceptionFilter]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            Logger.LogInformation($"Get request for register with data {model.Name} " +
                                  $"{model.Surname} and nickname {model.NickName}");
            if (!ModelState.IsValid)
            {
                Logger.LogInformation("Register model validation error");
                return View(model);
            }
            try
            {
                await _userService.CreateAsync(model.Name, model.FatherName, model.Surname,
                    model.NickName, model.Password, model.Email);
                var user = _mapper.Map<User, UserModel>
                (_userService.GetWithInclude
                    (_userService.Get(u => u.NickName == model.NickName && u.Password == model.Password).Id)
                );
                await Authenticate(user);
                Logger.LogInformation($"Success authenticate user with nickname {user.NickName}");
                return RedirectToAction("Index", "BusyRecords");
            }
            catch (ArgumentException e)
            {
                Logger.LogInformation(e.Message);
                ModelState.AddModelError("", "Bad login or password");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            Logger.LogInformation($"Logout user {GetUserNickNameFromContext()}");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
