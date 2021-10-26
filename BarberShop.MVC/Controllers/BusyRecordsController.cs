using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Filters;
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
        private readonly IOfferService _offerService;
        private readonly IMapper _mapper;

        public BusyRecordsController(IBusyRecordService busyService, 
            IBarberService barberService,
            IOfferService offerService,
            IMapper mapper)
        {
            _barberService = barberService;
            _offerService = offerService;
            _busyService = busyService;
            _mapper = mapper;
        }

        private async Task<Tuple<IEnumerable<BarberModel>, IEnumerable<ServiceModel>>> GetViewData()
        {
            var barbers = await _barberService.GetAll();
            var services = await _offerService.GetAll();
            return new Tuple<IEnumerable<BarberModel>, IEnumerable<ServiceModel>>(
                _mapper.Map<IEnumerable<Barber>, IEnumerable<BarberModel>>(barbers),
                _mapper.Map<IEnumerable<Offer>, IEnumerable<ServiceModel>>(services)
            );
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            Logger.LogInformation($"Records startup request");
            return View(await GetViewData());
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        [CommonExceptionFilter]
        public async Task<IActionResult> Index(int barberId, int serviceId, string date)
        {
            // TODO: Make beautiful calendar with hours
            var date1 = DateTime.Parse(date + " 00:00 AM", new CultureInfo("en-US"));
            Logger.LogInformation($"Record request with barber id: {barberId}, date {date}");
            
            var tupleModel = await GetViewData();
            if (_busyService.IsExists(barberId, date1) != null)
            {
                ViewBag.Message = "Sorry, this record exist";
                Logger.LogInformation($"Tried record to exist time with barber id: {barberId}, date {date}");
                return View(tupleModel);
            }

            var barber = await _barberService.GetById(barberId);
            var service = await _offerService.GetById(serviceId);

            var record = new BusyRecord()
            {
                BarberId = barberId,
                Barber = barber,
                RecordTime = date1,
                ServiceId = serviceId,
                Offer = service,
            };

            var validator = new BusyRecordsValidator();
            var validationResult = await validator.ValidateAsync(record);
            if (!validationResult.IsValid)
            {
                string msg = validationResult.Errors.First().ToString();
                Logger.LogInformation($"In record Validation error {msg}");
                ViewBag.Message = msg;
                return View(tupleModel);
            }

            await _busyService.Create(record);
            Logger.LogInformation($"Success record with barber id: {barberId}, date {date}");
            ViewBag.Message = $"Success record to barber: {barber.Name + barber.Surname}";
            return View(tupleModel);
        }
    }
}
