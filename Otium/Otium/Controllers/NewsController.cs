﻿using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Otium.Domain.Models;
using Otium.Models;
using Otium.Services.Abstractions;

namespace Otium.Controllers;

public class NewsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INewsService _newsService;

    public NewsController(ILogger<HomeController> logger, INewsService newsService)
    {
        _logger = logger;
        _newsService = newsService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var news = await _newsService.GetNewsAsync();
        if (news.StatusCode != HttpStatusCode.OK)
            return Error();
        
        var viewModel = new NewsViewModel
        {
            News = news.Data
        };
        return View(viewModel);
    }
    
    [HttpGet("/Post/{id}")]
    public async Task<IActionResult> Post(int id)
    {
        var news = await _newsService.GetNewsByIdAsync(id);
        return news.StatusCode != HttpStatusCode.OK 
            ? Error() 
            : View(news.Data);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error", new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}