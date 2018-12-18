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

        #region AJAX API
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

        [HttpGet]
        public IActionResult GetAllPage(int? categoryId, string keyWord, int indexCurrentPage, int pageSize)
        {
            Utilities.Dtos.PageResult<Application.ViewModels.ProductViewModel> model = _productService.GetAllPage(categoryId: categoryId, keyWord: keyWord, indexCurrentPage: indexCurrentPage, pageSize: pageSize);
            return new OkObjectResult(model);
        }
        #endregion AJAX API

    }
}