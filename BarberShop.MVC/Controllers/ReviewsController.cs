using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using BarberShop.MVC.Controllers.Base;
using BarberShop.MVC.Filters;
using BarberShop.MVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace BarberShop.MVC.Controllers
{
    [Authorize]
    public class ReviewsController : BaseController
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
        [ExceptionFilter]
        public async Task<IActionResult> Add()
        {
            IEnumerable<BarberModel> barbers = _mapper.Map<IEnumerable<Barber>, 
                IEnumerable<BarberModel>>(await _barberService.GetAllAsync());
            return View(barbers);
        }

        [HttpPost]
        [Authorize]
        [ExceptionFilter]
        public async Task<IActionResult> Add(string reviewText, int barberId)
        {
            await _reviewService.CreateAsync(barberId, reviewText, GetUserNickNameFromContext());
            ViewBag.Message = "Success add review. Thanks for your attention";
            return RedirectToAction("Index", "Reviews");
        }

        [HttpGet]
        [Authorize]
        [ExceptionFilter]
        public async Task<IActionResult> Remove(int id)
        {
            var userRole = GetUserRoleFromContext();
            var userNickName = GetUserNickNameFromContext();
            await _reviewService.DeleteAsync(id, userRole, userNickName);
            return RedirectToAction("Index", "Reviews");
        }

        [Authorize]
        [ExceptionFilter]
        public async Task<IActionResult> Edit(ReviewModel reviewModel)
        {
            switch (HttpContext.Request.Method.ToLower())
            {
                case "get":
                    IEnumerable<BarberModel> barbers = _mapper.Map<IEnumerable<Barber>,
                        IEnumerable<BarberModel>>(await _barberService.GetAllAsync());
                    ViewData["Barbers"] = barbers;
                    return View(reviewModel);
                case "post":
                    await _reviewService.UpdateAsync(_mapper.Map<ReviewModel, Review>(reviewModel));
                    break;
            } 
            return RedirectToAction("Index", "Reviews");
        }
    }
}
