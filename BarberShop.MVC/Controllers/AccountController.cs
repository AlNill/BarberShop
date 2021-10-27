using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
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
    public class AccountController : Controller
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
        [CommonExceptionFilter]
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
                    return RedirectToAction("Index", "BusyRecords");
                }
                ModelState.AddModelError("", "Bad login or password");
            }
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
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [CommonExceptionFilter]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            if (_userService.Get(u => u.NickName == model.NickName) == null)
            {
                var user = new UserModel()
                {
                    Name = model.Name,
                    FatherName = model.FatherName,
                    Surname = model.Surname,
                    NickName = model.NickName,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber
                };
                await _userService.Create(_mapper.Map<UserModel, User>(user));
                user = _mapper.Map<User, UserModel>
                (_userService.GetWithInclude
                    (_userService.Get(u => u.NickName == user.NickName && u.Password == user.Password).Id)
                );
                await Authenticate(user);
                return RedirectToAction("Index", "BusyRecords");
            }
            ModelState.AddModelError("", "Bad login or password");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
