﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Controllers.Base;
using BarberShop.MVC.Filters;
using BarberShop.MVC.Models;
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

        private async Task<Tuple<IEnumerable<BarberModel>, IEnumerable<OfferModel>>> GetViewData()
        {
            var barbers = await _barberService.GetAllAsync();
            var services = await _offerService.GetAll();
            return new Tuple<IEnumerable<BarberModel>, IEnumerable<OfferModel>>(
                _mapper.Map<IEnumerable<Barber>, IEnumerable<BarberModel>>(barbers),
                _mapper.Map<IEnumerable<Offer>, IEnumerable<OfferModel>>(services)
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
        [ExceptionFilter]
        public async Task<IActionResult> Index(int barberId, int offerId, DateTime date)
        {
            Logger.LogInformation($"Record request with barber id: {barberId}, date {date}");
            var tupleModel = await GetViewData();
            var barber = await _barberService.GetAsync(barberId);
            var offer = await _offerService.GetById(offerId);

            // TODO: Make validator
            //var validator = new BusyRecordsValidator();
            //var validationResult = await validator.ValidateAsync(record);
            //if (!validationResult.IsValid)
            //{
            //    string msg = validationResult.Errors.First().ToString();
            //    Logger.LogInformation($"In record Validation error {msg}");
            //    ViewBag.Message = msg;
            //    return View(tupleModel);
            //}
            try
            {
                await _busyService.CreateAsync(barberId, offerId, date);
                Logger.LogInformation($"Success record with barber id: {barberId}, date {date}");

                var user = await GetUserByNickName();
                _emailNotificator.SmtpNotify("Black rock record",
                    $"You are recorded for {offer.Title} to barber {barber.Name} {barber.Surname} on {date}." +
                    $"Have a nice day!", user.Email);

                ViewBag.Message = $"Success record to barber: {barber.Name + barber.Surname}";
            }
            catch (ArgumentException e)
            {
                ViewBag.Message = e.Message;
                Logger.LogInformation($"Tried record to exist time with barber id: {barberId}, date {date}");
            }
            return View(tupleModel);
        }
    }
}
