using Microsoft.AspNetCore.Mvc;

namespace UsedCars.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult MyIndex()
        {
            return View("Index");
        }
    }
}