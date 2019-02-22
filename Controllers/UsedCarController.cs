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
using System;

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
            IActionResult result;
            try
            {
                result = Ok(this.serviceCar.GetCars().ToList());
            }
            catch(Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            return result;
        }

        [HttpGet("{id}")]
        public IActionResult GetCar(int id)
        {
            IActionResult result = NotFound();
            try
            {
                Car car = this.serviceCar.GetCarByID(id);
                if(car != null)
                    result = Ok(car);
                //result = car == null ? NotFound() : Ok(car);
            }
            catch(Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            return result;
        }

        [HttpPost]
        public IActionResult PostCar(Car car)
        {
            IActionResult result;
            try
            {
                this.serviceCar.InsertCar(car);
                result = CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
            }
            catch(Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            return result;
        }

        [HttpPut]
        public IActionResult PutCar(Car car)
        {
            IActionResult result;
            try
            {
                this.serviceCar.UpdateCar(car);
                result = CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
            }
            catch(Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            return result;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            IActionResult result;
            try
            {
                this.serviceCar.DeleteCar(id);
                result = CreatedAtAction(nameof(GetCar), new { id = id });
            }
            catch(Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            return result;
        }
    }
}