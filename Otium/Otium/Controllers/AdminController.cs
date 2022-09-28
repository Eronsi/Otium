using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otium.Domain.ViewModels;
using Otium.Services.Abstractions;

namespace Otium.Controllers;

public class AdminController : Controller
{
    private readonly IAccountService _accountService;
    private readonly INewsService _newsService;

    public AdminController(IAccountService accountService, INewsService newsService)
    {
        _accountService = accountService;
        _newsService = newsService;
    }

    [HttpGet, Authorize(Roles = "Admin")]
    public IActionResult Index() => RedirectToAction("News");

    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<IActionResult> News()
    {
        var news = await _newsService.GetNewsAsync();
        return View(news);
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null) =>
        User.Identity!.IsAuthenticated
            ? RedirectToAction("Index")
            : View(new LoginViewModel { ReturnUrl = returnUrl });

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) 
            return View(model);

        var response = await _accountService.LoginAsync(model);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response.Data!));

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);
            return RedirectToAction("Index");
        }
        ModelState.AddModelError("", response.Message);
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}