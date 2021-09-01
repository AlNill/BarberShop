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
using Microsoft.Extensions.Logging;

namespace BarberShop.MVC.Controllers
{
    [Authorize]
    public class BusyRecordsController : Controller
    {
        private readonly IBarberService _barberService;
        private readonly IBusyRecordService _busyService;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public BusyRecordsController(IBusyRecordService busyService, IBarberService barberService, IMapper mapper,
            ILoggerService logger)
        {
            _barberService = barberService;
            _busyService = busyService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.LogInformation($"Records startup request");
            var barbers = _barberService.GetAll();
            return View(_mapper.Map<IEnumerable<Barber>, IEnumerable<BarberModel>>(barbers));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Index(int barberId, DateTime date)
        {
            _logger.LogInformation($"Record request with barber id: {barberId}, date {date}");
            var barbers = _barberService.GetAll();
            var result = _busyService.IsExists(barberId, date);
            if (result != null)
            {
                ViewBag.Message = "Sorry, this record exist";
                _logger.LogInformation($"Tried record to exist time with barber id: {barberId}, date {date}");
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
                string msg = validationResult.Errors.First().ToString();
                _logger.LogInformation($"In record Validation error {msg}");
                ViewBag.Message = msg;
                return View(_mapper.Map<IEnumerable<Barber>, IEnumerable<BarberModel>>(barbers));
            }

            _busyService.Create(record);
            _logger.LogInformation($"Success record with barber id: {barberId}, date {date}");
            return View(_mapper.Map<IEnumerable<Barber>, IEnumerable<BarberModel>>(barbers));
        }
    }
}
