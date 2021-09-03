using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.BLL.Services;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BarberShop.MVC.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public ReviewsController(IReviewService reviewService, IMapper mapper, ILoggerService logger)
        {
            _reviewService = reviewService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.LogInformation($"Get request for reviews get all");
            var reviews = _reviewService.GetAll();
            return View(_mapper.Map<IEnumerable<ReviewModel>>(reviews));
        }
    }
}
