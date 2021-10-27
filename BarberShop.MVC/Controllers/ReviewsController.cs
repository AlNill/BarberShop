using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Filters;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace BarberShop.MVC.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IBarberService _barberService;

        public ReviewsController(IReviewService reviewService, IMapper mapper,
            IUserService userService, IBarberService barberService)
        {
            _reviewService = reviewService;
            _mapper = mapper;
            _userService = userService;
            _barberService = barberService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var reviews = _reviewService.GetAll();
            return View(_mapper.Map<IEnumerable<ReviewModel>>(reviews));
        }

        [HttpGet]
        [Authorize]
        [CommonExceptionFilter]
        public async Task<IActionResult> Add()
        {
            IEnumerable<BarberModel> barbers = _mapper.Map<IEnumerable<Barber>, 
                IEnumerable<BarberModel>>(await _barberService.GetAll());
            return View(barbers);
        }

        [HttpPost]
        [Authorize]
        [CommonExceptionFilter]
        public async Task<IActionResult> Add(string reviewText, int barberId)
        {
            var review = new ReviewModel()
            {
                BarberId = _mapper.Map<Barber, BarberModel>(_barberService.GetById(barberId).Result).Id,
                UserReview = reviewText,
                UserId = _userService.Get(u => u.NickName == User.Identity.Name).Id
            };

            await _reviewService.Create(_mapper.Map<ReviewModel, Review>(review));
            ViewBag.Message = "Success add review. Thanks for your attention";
            return RedirectToAction("Index", "Reviews");
        }
    }
}
