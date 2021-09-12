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
    public class BarbersController: BaseController
    {
        private readonly IBarberService _barbersService;
        private readonly IMapper _mapper;

        public BarbersController(IBarberService barbersService, IMapper mapper)
        {
            _barbersService = barbersService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            Logger.LogInformation($"Get request for Barbers get all");
            var barbers = _barbersService.GetAll();
            return View(_mapper.Map<IEnumerable<BarberModel>>(barbers));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(BarberModel barber)
        {
            if (!ModelState.IsValid)
            {
                Logger.LogInformation($"Bad information for barber add: Name {barber.Name}, Surname {barber.Surname}," +
                                      $"FatherName {barber.FatherName}, Information {barber.Information}");
                ModelState.AddModelError("", "Badly input information");
                return View();
            }
            _barbersService.Create(_mapper.Map<BarberModel, Barber>(barber));
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(BarberModel barber)
        {
            if (HttpContext.Request.Method.ToLower().Equals("get"))
            {
                Logger.LogInformation("Get request for Edit Barber");
                if (ModelState.IsValid)
                {
                    Logger.LogInformation("Valid model. Send rendered view for editing");
                    return View(barber);
                }
                Logger.LogInformation("Bad barber model for editing");
                ModelState.AddModelError("", "Bad barber model");
                return RedirectToAction("Index", "Barbers");
            }
            else if (HttpContext.Request.Method.ToLower().Equals("post"))
            {
                Logger.LogInformation("Post request for Edit Barber");
                if (ModelState.IsValid)
                {
                    _barbersService.Update(_mapper.Map<BarberModel, Barber>(barber));
                    Logger.LogInformation($"Success update barber to: name {barber.Name}, surname {barber.Surname}, " +
                                          $"{barber.FatherName}");
                    return RedirectToAction("Index", "Barbers");
                }

                Logger.LogInformation("Model for edit barber is not valid");
                ModelState.AddModelError("", "Bad barber model");
                return RedirectToAction("Index", "Barbers");
            }
            ModelState.AddModelError("", "Non supported request method");
            return RedirectToAction("Index", "Barbers");
        }
    }
}