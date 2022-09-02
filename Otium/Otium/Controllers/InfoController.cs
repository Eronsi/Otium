using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Otium.Models;

namespace Otium.Controllers;

public class InfoController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public InfoController(ILogger<HomeController> logger) => 
        _logger = logger;

    [HttpGet]
    public IActionResult About() => View();
    
    [HttpGet]
    public IActionResult Businesses() => View();

    [HttpGet]
    public IActionResult Natural() => View();

    [HttpGet]
    public IActionResult Payment() => View();

    [HttpGet]
    public IActionResult Contact() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error", new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}