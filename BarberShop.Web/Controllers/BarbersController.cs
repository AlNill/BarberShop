using System.Collections.Generic;
using BarberShop.BLL.Interfaces;
using BarberShop.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarbersController: ControllerBase
    {
        readonly IBarberService _service;

        public BarbersController(IBarberService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Barber>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public ActionResult<Barber> AddBarber(Barber barber)
        {
            _service.Create(barber);
            return Ok(barber);
        }
    }
}
