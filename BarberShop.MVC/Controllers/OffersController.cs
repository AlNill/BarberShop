using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Controllers.Base;
using BarberShop.MVC.Filters;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Mvc;

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
                services = await _offerService.GetAll();
                return View(_mapper.Map<IEnumerable<Offer>, IEnumerable<OfferModel>>(services));
            }

            services = _offerService.GetServicesForSubTitle((string)serviceTitleSubstr);
            return View(_mapper.Map<IEnumerable<Offer>, IEnumerable<OfferModel>>(services));
        }

        [HttpGet]
        public IActionResult AdvancedSearch()
        {
            return View();
        }

        [HttpPost]
        [ExceptionFilter]
        public IActionResult AdvancedSearch(OfferModel offerSearchParams)
        {
            var services = _mapper.Map<IEnumerable<Offer>,
                IEnumerable<OfferModel>>(_offerService.AdvancedSearch(
                _mapper.Map<OfferModel, Offer>(offerSearchParams))
            );
            return View("Index", services);
        }
    }
}
