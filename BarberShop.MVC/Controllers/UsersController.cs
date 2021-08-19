using System.Collections.Generic;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAll();
            return View(_mapper.Map<IEnumerable<UserModel>>(users));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            return View(_mapper.Map<UserModel>(user));
        }

        [HttpPost]
        public IActionResult Edit(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                _userService.Create(_mapper.Map<User>(userModel));
                return RedirectToAction("Index");
            }
            return View(userModel);
        }
    }
}
