using Microsoft.AspNetCore.Mvc;

namespace Otium.Controllers;

public class ErrorController : Controller
{
    [HttpGet("Error/{statusCode:int}")]
    public IActionResult Index(int statusCode) =>
        View(statusCode);
}