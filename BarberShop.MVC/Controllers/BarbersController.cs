using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Controllers.Base;
using BarberShop.MVC.Filters;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Index()
        {
            Logger.LogInformation("Get request to barbers index");
            var barbers = await _barbersService.GetAllAsync();
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
        [ExceptionFilter]
        public async Task<IActionResult> Add(BarberModel barberModel, IFormFile image)
        {
            Logger.LogInformation($"Request to add barber {barberModel.Name} {barberModel.Surname}");
            var barber = _mapper.Map<BarberModel, Barber>(barberModel);
            if (image != null)
            {
                Logger.LogInformation("Request contains image");
                await _barbersService.SaveAvatarAsync(barber, image);
            }

            if (!ModelState.IsValid)
            {
                Logger.LogInformation("Add barber model not valid");
                ModelState.AddModelError("", "Badly input information");
                return View();
            }

            await _barbersService.CreateAsync(barber);
            Logger.LogInformation($"Successfully barber created {barberModel.Name} {barberModel.Surname}");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ExceptionFilter]
        public IActionResult Edit(BarberModel barber)
        {
            if (ModelState.IsValid)
                return View(barber);

            ModelState.AddModelError("", "Bad barber model");
            return RedirectToAction("Index", "Barbers");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ExceptionFilter]
        public async Task<IActionResult> Edit(BarberModel barberModel, IFormFile image)
        {
            Logger.LogInformation($"Edit barber with id: {barberModel.Id} to name: {barberModel.Name}," +
                                  $" Surname: {barberModel.Surname}, Information: {barberModel.Information}.");

            var barber = _mapper.Map<BarberModel, Barber>(barberModel);
            if (image != null)
                await _barbersService.SaveAvatarAsync(barber, image);

            if (ModelState.IsValid)
            {
                await _barbersService.UpdateAsync(barber);
                return RedirectToAction("Index", "Barbers");
            }

            ModelState.AddModelError("", "Bad barber model");
            return RedirectToAction("Index", "Barbers");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ExceptionFilter]
        public async Task<IActionResult> Remove(int id)
        {
            Logger.LogInformation($"Try to delete barber with barber id {id}");
            await _barbersService.DeleteAsync(id);
            return RedirectToAction("Index", "Barbers");
        }
    }
}