using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Controllers.Base;
using BarberShop.MVC.Filters;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.MVC.Controllers
{
    [Authorize]
    public class OffersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IOfferService _offerService;

        public OffersController(IMapper mapper, IOfferService offerService)
        {
            _mapper = mapper;
            _offerService = offerService;
        }

        [HttpGet]
        [ExceptionFilter]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string? serviceTitleSubstr)
        {
            IEnumerable<Offer> services;
            if (serviceTitleSubstr == null)
            {
                services = await _offerService.GetAllAsync();
                return View(_mapper.Map<IEnumerable<Offer>, IEnumerable<OfferModel>>(services));
            }

            services = _offerService.GetServicesForSubTitle((string)serviceTitleSubstr);
            return View(_mapper.Map<IEnumerable<Offer>, IEnumerable<OfferModel>>(services));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AdvancedSearch()
        {
            return View();
        }

        [HttpPost]
        [ExceptionFilter]
        [AllowAnonymous]
        public IActionResult AdvancedSearch(OfferModel offerSearchParams)
        {
            var services = _mapper.Map<IEnumerable<Offer>,
                IEnumerable<OfferModel>>(_offerService.AdvancedSearch(
                _mapper.Map<OfferModel, Offer>(offerSearchParams))
            );
            return View("Index", services);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ExceptionFilter]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(OfferModel offerModel)
        {
            await _offerService.CreateAsync(_mapper.Map<OfferModel, Offer>(offerModel));
            return RedirectToAction("Index", "Offers");
        }

        [HttpPost]
        [ExceptionFilter]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            await _offerService.DeleteAsync(id);
            return RedirectToAction("Index", "Offers");
        }

        [ExceptionFilter]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(OfferModel offerModel)
        {
            switch (HttpContext.Request.Method.ToLower())
            {
                case "get":
                    return View(offerModel);
                case "post":
                    await _offerService.UpdateAsync(_mapper.Map<OfferModel, Offer>(offerModel));
                    break;
            }
            return RedirectToAction("Index", "Offers");
        }
    }
}
