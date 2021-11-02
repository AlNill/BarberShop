﻿using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Controllers.Base;
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
        public async Task<IActionResult> Index(int pageSize = 10, int page = 0)
        {
            Logger.LogInformation($"Get request for Users page {page}");
            var pageViewModel = new PageModel(await _userService.GetCount(), page, pageSize);
            var viewModel = new IndexModel
            {
                PageModel = pageViewModel,
                Users = _mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(
                    _userService.GetRange(page * pageSize, pageSize))
            };
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Logger.LogInformation($"Edit request for user with id {id}");
            var user = await _userService.GetById(id);
            return View(_mapper.Map<UserModel>(user));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ExceptionFilter]
        public IActionResult Edit(UserModel userModel)
        {
            Logger.LogInformation($"Update user with id {userModel.Id}");
            if (ModelState.IsValid)
            {
                _userService.Update(_mapper.Map<User>(userModel));
                return RedirectToAction("Index");
            }
            return View(userModel);
        }
    }
}
