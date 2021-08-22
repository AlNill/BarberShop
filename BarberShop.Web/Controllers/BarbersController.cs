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
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Barber> Get(int id)
        {
            var barber = _service.GetById(id);
            if (barber == null)
                return NotFound();
            return Ok(barber);
        }

        [HttpPost]
        public ActionResult<Barber> AddBarber(Barber barber)
        {
            _service.Create(barber);
            return Ok(barber);
        }

        [HttpPut]
        public ActionResult<Barber> ChangeBarber(Barber barber)
        {
            if (_service.GetById(barber.Id) == null)
                return NotFound();
            _service.Update(barber);
            return Ok(barber);
        }
    }
}
