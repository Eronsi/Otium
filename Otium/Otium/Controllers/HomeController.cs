using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Otium.Domain.Models;
using Otium.Domain.ViewModels;
using Otium.Services.Abstractions;

namespace Otium.Controllers;

public class HomeController : Controller
{
    private readonly ICategoriesService _categoriesService;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public HomeController(ICategoriesService categoriesService, IEmailService emailService, 
        IConfiguration configuration)
    {
        _categoriesService = categoriesService;
        _emailService = emailService;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _categoriesService.GetAllCategoriesAsync();
        if (categories.StatusCode != HttpStatusCode.OK)
            return Error();
        
        var categoriesCount = categories.Data!.Count;
        var rowsCount = categoriesCount / 3;
        var remainColumnsCount = categoriesCount % 3;

        var viewModel = new IndexViewModel
        {
            CategoriesCount = categoriesCount,
            Categories = categories.Data!,
            RowsCount = rowsCount,
            RemainColumnsCount = remainColumnsCount
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Callback(string name, string phone, string text)
    {
        var email = new Email
        {
            Subject = "Заказ обратного звонка",
            To = _configuration.GetSection("Email:CallbackTo").Value,
            Text = $"Имя: {name}<br>Телефон: {phone}<br>Текст: {text}"
        };
        var result = await _emailService.SendEmailAsync(email);
        if (result.StatusCode != HttpStatusCode.OK)
            return Content(result.Message);
        
        return Content("Обратный звонок успешно заказан.<br>В ближайшее время мы свяжемся с вами." +
                       $"<br>Номер вашкей заявки: {result.Data}");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error", new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}