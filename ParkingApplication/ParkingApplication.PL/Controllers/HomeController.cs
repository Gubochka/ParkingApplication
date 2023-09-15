using Microsoft.AspNetCore.Mvc;

namespace ParkingApplication.Controllers;

[ApiController]
public class HomeController : Controller
{
    [Route("")]
    public IActionResult Index()
    {
        return View("Main");
    }

    [Route("authorization")]
    public IActionResult Authorization()
    {
        return View("Authorization");
    }
}