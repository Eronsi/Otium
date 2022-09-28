using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Otium.Controllers;

public class ErrorController : Controller
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [HttpGet("Error/{statusCode:int}")]
    public IActionResult Index(int statusCode)
    {   
        _logger.Log(LogLevel.Warning, $"RequestId: {Activity.Current?.Id}");
        return View(statusCode);
    }
}