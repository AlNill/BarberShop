using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Controllers.Base;
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
        private readonly IUserService _userService;
        private readonly IEmailNotificator _emailNotificator;
        private readonly IMapper _mapper;

        public BusyRecordsController(IBusyRecordService busyService, 
            IBarberService barberService,
            IOfferService offerService,
            IUserService userService,
            IEmailNotificator emailNotificator,
            IMapper mapper)
        {
            _barberService = barberService;
            _offerService = offerService;
            _busyService = busyService;
            _userService = userService;
            _emailNotificator = emailNotificator;
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


        private async Task<UserModel> GetUserByNickName()
        {
            var nickName = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            return _mapper.Map<User, UserModel>(await _userService.GetByNickName(nickName));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        [CommonExceptionFilter]
        public async Task<IActionResult> Index(int barberId, int offerId, DateTime date)
        {
            Logger.LogInformation($"Record request with barber id: {barberId}, date {date}");
            var tupleModel = await GetViewData();
            if (_busyService.IsExists(barberId, date) != null)
            {
                ViewBag.Message = "Sorry, this record exist";
                Logger.LogInformation($"Tried record to exist time with barber id: {barberId}, date {date}");
                return View(tupleModel);
            }

            var barber = await _barberService.GetById(barberId);
            var offer = await _offerService.GetById(offerId);

            var record = new BusyRecord()
            {
                BarberId = barberId,
                Barber = barber,
                RecordTime = date,
                ServiceId = offerId,
                Offer = offer,
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

            var user = await GetUserByNickName();
            _emailNotificator.SmtpNotify("Black rock record",
                $"You are recorded for {offer.Title} to barber {barber.Name} {barber.Surname} on {date}." +
                $"Have a nice day!", user.Email);

            ViewBag.Message = $"Success record to barber: {barber.Name + barber.Surname}";
            return View(tupleModel);
        }
    }
}
