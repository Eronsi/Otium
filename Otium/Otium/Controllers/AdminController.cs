using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otium.Domain.Models;
using Otium.Domain.ViewModels;
using Otium.Services.Abstractions;

namespace Otium.Controllers;

public class AdminController : Controller
{
    private readonly IAccountService _accountService;
    private readonly INewsService _newsService;
    private readonly ICategoriesService _categoriesService;

    public AdminController(IAccountService accountService, INewsService newsService, 
        ICategoriesService categoriesService)
    {
        _accountService = accountService;
        _newsService = newsService;
        _categoriesService = categoriesService;
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
    
    [HttpPost, Authorize(Roles = "Admin"), Route("Admin/News/Remove/")]
    public async Task<IActionResult> RemoveNews(int id)
    {
        var response = await _newsService.DeleteNewsAsync(id);
        if (response.StatusCode != HttpStatusCode.OK)
            return Json(response);
        return RedirectToAction("News");
    }

    [HttpGet, Authorize(Roles = "Admin"), Route("Admin/News/Update/{id:int}")]
    public async Task<IActionResult> UpdateNewsView(int id)
    {
        var response = await _newsService.GetNewsByIdAsync(id);
        return View("UpdateNews", response.StatusCode == HttpStatusCode.OK
            ? new UpdateNewsViewModel {IsNew = false, News = response.Data!}
            : new UpdateNewsViewModel {IsNew = true, News = new News {Id = id}});
    }

    [HttpPost, Authorize(Roles = "Admin"), Route("Admin/News/Update/")]
    public async Task<IActionResult> UpdateNews(UpdateNewsViewModel model)
    {
        if (!ModelState.IsValid) return View("UpdateNews", model);

        var response =  model.IsNew 
            ? await _newsService.AddNewsAsync(model.News!)
            : await _newsService.UpdateNewsAsync(model.News!);
        if (response.StatusCode != HttpStatusCode.OK)
            return Json(response);

        return RedirectToAction("News");
    }
    
    [HttpGet, Authorize(Roles = "Admin"), Route("Admin/Categories/")]
    public async Task<IActionResult> Categories()
    {
        var response = await _categoriesService.GetAllCategoriesAsync();
        return View(response);
    }
    
    [HttpPost, Authorize(Roles = "Admin"), Route("Admin/Categories/Remove/")]
    public async Task<IActionResult> RemoveCategory(int id)
    {
        var response = await _categoriesService.DeleteCategoryAsync(id);
        if (response.StatusCode != HttpStatusCode.OK)
            return Json(response);
        return RedirectToAction("Categories");
    }
    
    [HttpGet, Authorize(Roles = "Admin"), Route("Admin/Categories/Update/{id:int}")]
    public async Task<IActionResult> UpdateCategoryView(int id)
    {
        var response = await _categoriesService.GetCategoryByIdAsync(id);
        return View("UpdateCategory", response.StatusCode == HttpStatusCode.OK
            ? new UpdateCategoryViewModel {IsNew = false, Category = response.Data!}
            : new UpdateCategoryViewModel {IsNew = true, Category = new Categories {Id = id}});
    }
    
    [HttpPost, Authorize(Roles = "Admin"), Route("Admin/Categories/Update/")]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryViewModel model)
    {
        if (!ModelState.IsValid) return View("UpdateCategory", model);

        var response =  model.IsNew 
            ? await _categoriesService.AddCategoryAsync(model.Category!)
            : await _categoriesService.UpdateCategoryAsync(model.Category!);
        if (response.StatusCode != HttpStatusCode.OK)
            return Json(response);

        return RedirectToAction("Categories");
    }
}