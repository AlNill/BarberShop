using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Filters;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BarberShop.MVC.Controllers
{
    public class ServicesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceService _service;

        public ServicesController(IMapper mapper, IServiceService service)
        {
            _mapper = mapper;
            _service = service;
        }

        public IActionResult Index(string? serviceTitleSubstr)
        {
            IEnumerable<Service> services;
            if (serviceTitleSubstr == null)
            {
                Logger.LogInformation($"Get request for Services get all");
                services = _service.GetAll();
                return View(_mapper.Map<IEnumerable<Service>, IEnumerable<ServiceModel>>(services));
            }

            services = _service.GetServicesForSubTitle((string)serviceTitleSubstr);
            return View(_mapper.Map<IEnumerable<Service>, IEnumerable<ServiceModel>>(services));
        }

        [HttpGet]
        public IActionResult AdvancedSearch()
        {
            return View();
        }

        [HttpPost]
        [CommonExceptionFilter]
        public IActionResult AdvancedSearch(ServiceModel serviceSearchParams)
        {
            var services = _mapper.Map<IEnumerable<Service>,
                IEnumerable<ServiceModel>>(_service.AdvancedSearch(
                _mapper.Map<ServiceModel, Service>(serviceSearchParams))
            );
            return View("Index", services);
        }
    }
}
