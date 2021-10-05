using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class BusyRecordsController : BaseController
    {
        private readonly IBarberService _barberService;
        private readonly IBusyRecordService _busyService;
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public BusyRecordsController(IBusyRecordService busyService, 
            IBarberService barberService,
            IServiceService serviceService,
            IMapper mapper)
        {
            _barberService = barberService;
            _serviceService = serviceService;
            _busyService = busyService;
            _mapper = mapper;
        }

        private Tuple<IEnumerable<BarberModel>, IEnumerable<ServiceModel>> GetViewData()
        {
            var barbers = _barberService.GetAll();
            var services = _serviceService.GetAll();
            return new Tuple<IEnumerable<BarberModel>, IEnumerable<ServiceModel>>(
                _mapper.Map<IEnumerable<Barber>, IEnumerable<BarberModel>>(barbers),
                _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceModel>>(services)
            );
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            Logger.LogInformation($"Records startup request");
            var barbers = _barberService.GetAll();
            return View(GetViewData());
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Index(int barberId, int serviceId, string date)
        {
            // TODO: Make beautiful calendar with hours
            var date1 = DateTime.Parse(date + " 00:00 AM", new CultureInfo("en-US"));
            Logger.LogInformation($"Record request with barber id: {barberId}, date {date}");
            var tupleModel = GetViewData();
            try
            {
                if (_busyService.IsExists(barberId, date1) != null)
                {
                    ViewBag.Message = "Sorry, this record exist";
                    Logger.LogInformation($"Tried record to exist time with barber id: {barberId}, date {date}");
                    return View(tupleModel);
                }

                var barber = _barberService.GetById(barberId);
                var service = _serviceService.GetById(serviceId);

                var record = new BusyRecord()
                {
                    BarberId = barberId,
                    Barber = barber,
                    RecordTime = date1,
                    ServiceId = serviceId,
                    Service = service,
                };

                var validator = new BusyRecordsValidator();
                var validationResult = validator.Validate(record);
                if (!validationResult.IsValid)
                {
                    string msg = validationResult.Errors.First().ToString();
                    Logger.LogInformation($"In record Validation error {msg}");
                    ViewBag.Message = msg;
                    return View(tupleModel);
                }

                _busyService.Create(record);
                Logger.LogInformation($"Success record with barber id: {barberId}, date {date}");
                ViewBag.Message = $"Success record to barber: {barber.Name + barber.Surname}";
                return View(tupleModel);
            }
            catch (Exception e)
            {
                Logger.LogError($"Recording error: {e.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}
