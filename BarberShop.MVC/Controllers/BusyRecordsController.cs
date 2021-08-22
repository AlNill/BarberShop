using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Models;
using BarberShop.MVC.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.MVC.Controllers
{
    [Authorize]
    public class BusyRecordsController : Controller
    {
        private readonly IBarberService _barberService;
        private readonly IBusyRecordService _busyService;
        private readonly IMapper _mapper;
        public BusyRecordsController(IBusyRecordService busyService,IBarberService barberService, IMapper mapper)
        {
            _barberService = barberService;
            _busyService = busyService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var barbers = _barberService.GetAll();
            return View(_mapper.Map<IEnumerable<Barber>, IEnumerable<BarberModel>>(barbers));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Index(int barberId, DateTime date)
        {
            var barbers = _barberService.GetAll();
            var result = _busyService.IsExists(barberId, date);
            if (result != null)
            {
                ViewBag.Message = "Sorry, this record exist";
                return View(_mapper.Map<IEnumerable<Barber>, IEnumerable<BarberModel>>(barbers));
            }
                
            var barber = _barberService.GetById(barberId);

            var record = new BusyRecord()
            {
                BarberId = barberId,
                Barber = barber,
                RecordTime = date,
            };

            var validator = new BusyRecordsValidator();
            var validationResult = validator.Validate(record);
            if (!validationResult.IsValid)
            {
                ViewBag.Message = validationResult.Errors.First().ToString();
                return View(_mapper.Map<IEnumerable<Barber>, IEnumerable<BarberModel>>(barbers));
            }

            _busyService.Create(record);
            return View(_mapper.Map<IEnumerable<Barber>, IEnumerable<BarberModel>>(barbers));
        }
    }
}
