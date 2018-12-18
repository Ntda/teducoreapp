using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TeduNetcore.Application.Interfaces;

namespace TeduNetcore.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private IProductService _productService;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Infor product: ");
            IList<Application.ViewModels.ProductViewModel> products = _productService.GetAll();
            foreach (Application.ViewModels.ProductViewModel product in products)
            {
                _logger.LogInformation(product.ToString());
            }
            return new OkObjectResult(products);
        }
    }
}