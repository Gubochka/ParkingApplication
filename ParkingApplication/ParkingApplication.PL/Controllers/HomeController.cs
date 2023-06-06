using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ParkingApplication.Models;

namespace ParkingApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

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


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}