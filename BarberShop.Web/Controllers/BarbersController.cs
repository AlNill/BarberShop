using System.Collections.Generic;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return Ok(_service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public ActionResult<Barber> Get(int id)
        {
            var barber = _service.GetAsync(id);
            if (barber == null)
                return NotFound();
            return Ok(barber);
        }

        [HttpPost]
        public ActionResult<Barber> AddBarber(Barber barber)
        {
            _service.CreateAsync(barber);
            return Ok(barber);
        }

        [HttpPut]
        public ActionResult<Barber> ChangeBarber(Barber barber)
        {
            if (_service.GetAsync(barber.Id) == null)
                return NotFound();
            _service.UpdateAsync(barber);
            return Ok(barber);
        }
    }
}
