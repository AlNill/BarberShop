using System.Collections.Generic;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BarberShop.MVC.Controllers
{
    [Authorize]
    public class BarbersController: Controller
    {
        private readonly IBarberService _barbersService;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public BarbersController(IBarberService barbersService, IMapper mapper, ILoggerService logger)
        {
            _barbersService = barbersService;
            _mapper = mapper;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.LogInformation($"Get request for Barbers get all");
            var barbers = _barbersService.GetAll();
            return View(_mapper.Map<IEnumerable<BarberModel>>(barbers));
        }
    }
}
