using System;
using System.Collections.Generic;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.BLL.Models;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.MVC.Controllers
{
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
        public IActionResult Index()
        {
            var barbers = _barberService.GetAll();
            return View(_mapper.Map<IEnumerable<Barber>, IEnumerable<BarberModel>>(barbers));
        }

        [HttpPost]
        public IActionResult Index(int barberId, DateTime date)
        {
            var result = _busyService.IsExists(barberId, date);
            if (result != null)
                throw new Exception("Bad date");
            var barber = _barberService.GetById(barberId);
            _busyService.Create(new BusyRecord()
            {
                BarberId = barberId,
                Barber = barber,
                RecordTime = date,
            });
            var barbers = _barberService.GetAll();
            return View(_mapper.Map<IEnumerable<Barber>, IEnumerable<BarberModel>>(barbers));
        }
    }
}
