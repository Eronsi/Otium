using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Otium.Domain.Models;
using Otium.Domain.ViewModels;
using Otium.Services.Abstractions;

namespace Otium.Controllers;

public class ProductsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductsService _productsService;
    private readonly ICategoriesService _categoriesService;
    private readonly IParamsService _paramsService;
    private readonly IParamsValuesService _paramsValuesService;

    public ProductsController(ILogger<HomeController> logger, IProductsService productsService, 
        ICategoriesService categoriesService, IParamsService paramsService, IParamsValuesService paramsValuesService)
    {
        _logger = logger;
        _productsService = productsService;
        _categoriesService = categoriesService;
        _paramsService = paramsService;
        _paramsValuesService = paramsValuesService;
    }
    
    [HttpGet("/Products/{categoryName}")]
    public async Task<IActionResult> Index(string categoryName)
    {
        var products = await _productsService.GetProductsByCategoryAsync(categoryName);
        if (products.StatusCode != HttpStatusCode.OK)
            return Error();
        
        var category = await _categoriesService.GetCategoryByNameAsync(categoryName);
        if (category.StatusCode != HttpStatusCode.OK)
            return Error();

        var viewModel = new ProductsViewModel
        {
            Category = category.Data,
            ProductsList = products.Data
        };
        
        return View(viewModel);
    }
    
    [HttpGet("/Products/{categoryName}/{productName}")]
    public async Task<IActionResult> Product(string categoryName, string productName)
    {
        var product = await _productsService.GetProductByNameAsync(productName);
        if (product.StatusCode != HttpStatusCode.OK)
            return Error();
        
        var category = await _categoriesService.GetCategoryByNameAsync(categoryName);
        if (category.StatusCode != HttpStatusCode.OK)
            return Error();

        var currentParams = await _paramsService.GetParamsAsync(product.Data!.Id);
        if (currentParams.StatusCode != HttpStatusCode.OK)
            return Error();

        var dict = new Dictionary<Params, List<ParamsValues>>();
        foreach (var param in currentParams.Data!)
        {
            var currentValues = await _paramsValuesService.GetParamValuesAsync(param.Id);
            if (currentValues.StatusCode != HttpStatusCode.OK)
                return Error();

            dict[param] = currentValues.Data!;
        }
        
        var viewModel = new ProductViewModel
        {
            Product = product.Data,
            Params = dict
        };
        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error", new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}