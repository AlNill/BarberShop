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
using Microsoft.Extensions.Logging;

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
            Logger.LogInformation($"Get request to review index");
            var reviews = _reviewService.GetAll();
            return View(_mapper.Map<IEnumerable<ReviewModel>>(reviews));
        }

        [HttpGet]
        [Authorize]
        [ExceptionFilter]
        public async Task<IActionResult> Add()
        {
            Logger.LogInformation($"Get request to add review");
            IEnumerable<BarberModel> barbers = _mapper.Map<IEnumerable<Barber>, 
                IEnumerable<BarberModel>>(await _barberService.GetAllAsync());
            return View(barbers);
        }

        [HttpPost]
        [Authorize]
        [ExceptionFilter]
        public async Task<IActionResult> Add(string reviewText, int barberId)
        {
            var userNickname = GetUserNickNameFromContext();
            Logger.LogInformation($"Post request to add review {reviewText} {barberId} from user {userNickname}");
            await _reviewService.CreateAsync(barberId, reviewText, userNickname);
            ViewBag.Message = "Success add review. Thanks for your attention";
            Logger.LogInformation($"Successfully added review from user {userNickname} " +
                                  $"with text {reviewText} to barber with id {barberId}");
            return RedirectToAction("Index", "Reviews");
        }

        [HttpGet]
        [Authorize]
        [ExceptionFilter]
        public async Task<IActionResult> Remove(int id)
        {
            Logger.LogInformation($"Get request to remove review with id {id}");
            var userRole = GetUserRoleFromContext();
            var userNickName = GetUserNickNameFromContext();
            await _reviewService.DeleteAsync(id, userRole, userNickName);
            Logger.LogInformation($"Successfully removed review with id {id}");
            return RedirectToAction("Index", "Reviews");
        }

        [Authorize]
        [ExceptionFilter]
        public async Task<IActionResult> Edit(ReviewModel reviewModel)
        {
            switch (HttpContext.Request.Method.ToLower())
            {
                case "get":
                    Logger.LogInformation($"Get request to edit review with text {reviewModel.UserReview} from user {reviewModel.User.NickName}");
                    IEnumerable<BarberModel> barbers = _mapper.Map<IEnumerable<Barber>,
                        IEnumerable<BarberModel>>(await _barberService.GetAllAsync());
                    ViewData["Barbers"] = barbers;
                    return View(reviewModel);
                case "post":
                    Logger.LogInformation($"Post request to edit review with id {reviewModel.Id} from user {reviewModel.User.NickName}");
                    await _reviewService.UpdateAsync(_mapper.Map<ReviewModel, Review>(reviewModel));
                    Logger.LogInformation($"Successfully updated review with id {reviewModel.Id} from user {reviewModel.User.NickName}");
                    break;
            } 
            return RedirectToAction("Index", "Reviews");
        }
    }
}
