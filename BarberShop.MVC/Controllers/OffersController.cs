using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Filters;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BarberShop.MVC.Controllers
{
    public class OffersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IOfferService _offerService;

        public OffersController(IMapper mapper, IOfferService offerService)
        {
            _mapper = mapper;
            _offerService = offerService;
        }

        public async Task<IActionResult> Index(string? serviceTitleSubstr)
        {
            IEnumerable<Offer> services;
            if (serviceTitleSubstr == null)
            {
                Logger.LogInformation($"Get request for Offers get all");
                services = await _offerService.GetAll();
                return View(_mapper.Map<IEnumerable<Offer>, IEnumerable<ServiceModel>>(services));
            }

            services = _offerService.GetServicesForSubTitle((string)serviceTitleSubstr);
            return View(_mapper.Map<IEnumerable<Offer>, IEnumerable<ServiceModel>>(services));
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
            var services = _mapper.Map<IEnumerable<Offer>,
                IEnumerable<ServiceModel>>(_offerService.AdvancedSearch(
                _mapper.Map<ServiceModel, Offer>(serviceSearchParams))
            );
            return View("Index", services);
        }
    }
}
