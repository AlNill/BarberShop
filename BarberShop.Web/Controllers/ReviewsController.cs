using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BarberShop.BLL.Interfaces;
using BarberShop.BLL.Models;

namespace BarberShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewsController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Review>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Review> Get(int id)
        {
            var review = _service.GetById(id);
            if (review == null)
                return NotFound();
            return Ok(review);
        }
    }
}
