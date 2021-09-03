using System;
using System.Collections.Generic;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BarberShop.MVC.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public UsersController(IUserService userService, IMapper mapper, ILoggerService logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            _logger.LogInformation($"Get request for users get all");
            var users = _userService.GetAll();
            return View(_mapper.Map<IEnumerable<UserModel>>(users));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            _logger.LogInformation($"Get request for edit user with user id {id}");
            var user = _userService.GetById(id);
            return View(_mapper.Map<UserModel>(user));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(UserModel userModel)
        {
            try
            {
                _logger.LogInformation($"Get request for edit user with user nickname {userModel.NickName}");
                if (ModelState.IsValid)
                {
                    _userService.Update(_mapper.Map<User>(userModel));
                    return RedirectToAction("Index");
                }
                return View(userModel);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error to edit user with user nickname {userModel.NickName}. " +
                                 $"Exception message: {e.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}
