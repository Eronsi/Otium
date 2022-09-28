using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Otium.Models;

namespace Otium.Controllers;

public class InfoController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public InfoController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult About() => View();
    
    [HttpGet]
    public IActionResult Businesses() => View();

    [HttpGet]
    public IActionResult Natural() => View();

    [HttpGet]
    public IActionResult Payment() => View();

    [HttpGet]
    public IActionResult Contact()
    {
        var viewModel = new ContactsViewModel
        {
            Phone1 = _configuration.GetSection("Contacts:Phone1").Value,
            Tg1 = _configuration.GetSection("Contacts:Tg1").Value,
            Phone2 = _configuration.GetSection("Contacts:Phone2").Value,
            Tg2 = _configuration.GetSection("Contacts:Tg2").Value,
            AdminEmail = _configuration.GetSection("Contacts:AdminEmail").Value
        };
        
        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error", new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}