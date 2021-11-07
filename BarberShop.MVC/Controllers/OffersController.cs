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
using Microsoft.Extensions.Logging;

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
        public async Task<IActionResult> Index(string offerTitleSubstr)
        {
            Logger.LogInformation($"Get request to offers index");
            IEnumerable<Offer> services;
            if (offerTitleSubstr == null)
            {
                services = await _offerService.GetAllAsync();
                return View(_mapper.Map<IEnumerable<Offer>, IEnumerable<OfferModel>>(services));
            }

            Logger.LogInformation($"Request to find offers with subtitle {offerTitleSubstr}");
            services = _offerService.GetServicesForSubTitle(offerTitleSubstr);
            return View(_mapper.Map<IEnumerable<Offer>, IEnumerable<OfferModel>>(services));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AdvancedSearch()
        {
            Logger.LogInformation($"Get request to offers advanced search");
            return View();
        }

        [HttpPost]
        [ExceptionFilter]
        [AllowAnonymous]
        public IActionResult AdvancedSearch(OfferParamsModel offerSearchParams)
        {
            Logger.LogInformation($"Advanced search in offers " +
                                  $"with min cost: {offerSearchParams.MinCost} " +
                                  $"max cost: {offerSearchParams.MaxCost} " +
                                  $"subtitle: {offerSearchParams.SubTitle}");
            var services = _mapper.Map<IEnumerable<Offer>,
                IEnumerable<OfferModel>>(_offerService.AdvancedSearch(
                offerSearchParams.SubTitle, offerSearchParams.MinCost, offerSearchParams.MaxCost
                )
            );
            Logger.LogInformation($"Successfully advanced search in offers");
            return View("Index", services);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            Logger.LogInformation($"Get request to add offer");
            return View();
        }

        [HttpPost]
        [ExceptionFilter]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(OfferModel offerModel)
        {
            Logger.LogInformation($"Request to add offer {offerModel.Title}");
            await _offerService.CreateAsync(_mapper.Map<OfferModel, Offer>(offerModel));
            Logger.LogInformation($"Successfully added offer {offerModel.Title}");
            return RedirectToAction("Index", "Offers");
        }

        [HttpPost]
        [ExceptionFilter]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            Logger.LogInformation($"Request to remove offer {id}");
            await _offerService.DeleteAsync(id);
            Logger.LogInformation($"Successfully removed offer {id}");
            return RedirectToAction("Index", "Offers");
        }

        [ExceptionFilter]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(OfferModel offerModel)
        {
            switch (HttpContext.Request.Method.ToLower())
            {
                case "get":
                    Logger.LogInformation($"Request to edit offer {offerModel.Title}");
                    return View(offerModel);
                case "post":
                    await _offerService.UpdateAsync(_mapper.Map<OfferModel, Offer>(offerModel));
                    Logger.LogInformation($"Success edit offer with id {offerModel.Id}");
                    break;
            }
            return RedirectToAction("Index", "Offers");
        }
    }
}
