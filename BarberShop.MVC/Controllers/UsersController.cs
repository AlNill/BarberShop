using System;
using System.Collections.Generic;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Filters;
using BarberShop.MVC.Models;
using BarberShop.MVC.Models.UsersPage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BarberShop.MVC.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index(int pageSize = 10, int page = 0)
        {
            Logger.LogInformation($"Get request for users get all");
            PageModel pageViewModel = new PageModel(_userService.GetCount(), page, pageSize);
            IndexModel viewModel = new IndexModel
            {
                PageModel = pageViewModel,
                Users = _mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(
                    _userService.GetRange(page * pageSize, pageSize))
            };
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Logger.LogInformation($"Get request for edit user with user id {id}");
            var user = _userService.GetById(id);
            return View(_mapper.Map<UserModel>(user));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [CommonExceptionFilter]
        public IActionResult Edit(UserModel userModel)
        {
            try
            {
                Logger.LogInformation($"Get request for edit user with user nickname {userModel.NickName}");
                if (ModelState.IsValid)
                {
                    _userService.Update(_mapper.Map<User>(userModel));
                    return RedirectToAction("Index");
                }
                return View(userModel);
            }
            catch (Exception e)
            {
                Logger.LogError($"Error to edit user with user nickname {userModel.NickName}. " +
                                $"Exception message: {e.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}
