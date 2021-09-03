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
    public class BarbersController: Controller
    {
        private readonly IBarberService _barbersService;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public BarbersController(IBarberService barbersService, IMapper mapper, ILoggerService logger)
        {
            _barbersService = barbersService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.LogInformation($"Get request for Barbers get all");
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
                _logger.LogInformation($"Bad information for barber add: Name {barber.Name}, Surname {barber.Surname}," +
                                       $"FatherName {barber.FatherName}, Information {barber.Information}");
                ModelState.AddModelError("", "Badly input information");
                return View();
            }
            _barbersService.Create(_mapper.Map<BarberModel, Barber>(barber));
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(BarberModel barber)
        {
            _logger.LogInformation("Get request for Edit Barber");
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Valid model. Send rendered view for editing");
                return View(barber);
            }
            _logger.LogInformation("Bad barber model for editing");
            ModelState.AddModelError("", "Bad barber model");
            return RedirectToAction("Index", "Barbers");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditBarber(BarberModel barber)
        {
            _logger.LogInformation("Post request for Edit Barber");
            if (ModelState.IsValid)
            {
                _barbersService.Update(_mapper.Map<BarberModel, Barber>(barber));
                _logger.LogInformation($"Success update barber to: name {barber.Name}, surname {barber.Surname}, " +
                                       $"{barber.FatherName}");
                return RedirectToAction("Index", "Barbers");
            }

            _logger.LogInformation("Model for edit barber is not valid");
            ModelState.AddModelError("", "Bad barber model");
            return RedirectToAction("Index", "Barbers");
        }
    }
}