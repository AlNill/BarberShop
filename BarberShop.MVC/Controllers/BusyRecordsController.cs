using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Filters;
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
        private readonly IOfferService _offerService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public BusyRecordsController(IBusyRecordService busyService, 
            IBarberService barberService,
            IOfferService offerService,
            IUserService userService,
            IMapper mapper)
        {
            _barberService = barberService;
            _offerService = offerService;
            _busyService = busyService;
            _userService = userService;
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
        public async Task<IActionResult> Index(int barberId, int serviceId, DateTime date)
        {
            var tupleModel = await GetViewData();
            if (_busyService.IsExists(barberId, date) != null)
            {
                ViewBag.Message = "Sorry, this record exist";
                return View(tupleModel);
            }

            var barber = await _barberService.GetById(barberId);
            var service = await _offerService.GetById(serviceId);

            var record = new BusyRecord()
            {
                BarberId = barberId,
                Barber = barber,
                RecordTime = date,
                ServiceId = serviceId,
                Offer = service,
            };

            var validator = new BusyRecordsValidator();
            var validationResult = await validator.ValidateAsync(record);
            if (!validationResult.IsValid)
            {
                string msg = validationResult.Errors.First().ToString();
                ViewBag.Message = msg;
                return View(tupleModel);
            }

            await _busyService.Create(record);
            ViewBag.Message = $"Success record to barber: {barber.Name + barber.Surname}";
            return View(tupleModel);
        }
    }
}
