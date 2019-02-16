using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedCars.Models;
using UsedCars.DAL;
using System.Net;
using System.Web.Http;

namespace UsedCars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsedCarController: ControllerBase
    {
        private CarRepository serviceCar;

        public UsedCarController()
        {
            //dependency injection to manage data
            this.serviceCar = new CarRepository(new ContextJsonResolver());
        }

        [HttpGet]
        public IActionResult GetCars()
        {
            return Ok(this.serviceCar.GetCars().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetCar(int id)
        {
            Car car = this.serviceCar.GetCarByID(id);

            if(car == null)
                return NotFound();
            
            return Ok(car);
        }

        [HttpPost]
        public IActionResult PostCar(Car car)
        {
            if(this.serviceCar.InsertCar(car))
                return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public IActionResult PutCar(Car car)
        {
            if(this.serviceCar.UpdateCar(car))
                return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            if(this.serviceCar.DeleteCar(id))
                return CreatedAtAction(nameof(GetCar), new { id = id }, id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}