using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.BLL.Services;
using BarberShop.MVC.Models;

namespace BarberShop.MVC.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var reviews = _reviewService.GetAll();
            return View(_mapper.Map<IEnumerable<ReviewModel>>(reviews));
        }
    }
}
