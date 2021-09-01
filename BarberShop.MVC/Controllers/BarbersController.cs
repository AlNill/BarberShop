using System.Collections.Generic;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.MVC.Controllers
{
    [Authorize]
    public class BarbersController: Controller
    {
        private readonly IBarberService _barbersService;
        private readonly IMapper _mapper;

        public BarbersController(IBarberService barbersService, IMapper mapper)
        {
            _barbersService = barbersService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var barbers = _barbersService.GetAll();
            return View(_mapper.Map<IEnumerable<BarberModel>>(barbers));
        }
    }
}
