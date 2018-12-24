using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Utilities.Helpers;

namespace TeduNetcore.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService, IProductCategoryService productCategoryService, ILogger<ProductController> logger)
        {
            _productCategoryService = productCategoryService;
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
        public IActionResult GetAllCategories()
        {
            List<Application.ViewModels.ProductCategoryViewModel> categories = _productCategoryService.GetAll();
            _logger.LogInformation("Infor categories:");
            categories.ForEach(c =>
            {
                _logger.LogInformation(c.ToString());
            });
            return new OkObjectResult(categories);
        }

        [HttpGet]
        public IActionResult GetAllPage(int? categoryId, string keyWord, int indexCurrentPage, int pageSize)
        {
            Utilities.Dtos.PageResult<Application.ViewModels.ProductViewModel> model = _productService.GetAllPage(categoryId: categoryId, keyWord: keyWord, indexCurrentPage: indexCurrentPage, pageSize: pageSize);
            return new OkObjectResult(model);
        }
        #endregion AJAX API

        [HttpPost]
        public IActionResult SaveEntity(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Microsoft.AspNetCore.Mvc.ModelBinding.ModelErrorCollection> allError = ModelState.Values.Select(err => err.Errors);
                return new BadRequestObjectResult(allError);
            }
            productViewModel.SeoAlias = TextHelper.ToUnsignString(productViewModel.Name);
            if (productViewModel.Id == 0)
            {
                _productService.Add(productViewModel);
            }
            else
            {
                _productService.Update(productViewModel);
            }
            _productService.Save();
            return new OkObjectResult(productViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _productService.Delete(id);
                _productService.Save();
                return new OkObjectResult(id);
            }
            else
            {
                return new BadRequestResult();
            }
        }

        public IActionResult GetById(int id)
        {
            if (ModelState.IsValid)
            {
                var product = _productService.GetById(id);
                _logger.LogInformation("Infor product:{0}", product.ToString());
                return new OkObjectResult(product);
            }
            else
            {
                var errors = ModelState.Values.Select(err => err.Errors);
                return new BadRequestObjectResult(errors);
            }
        }
    }
}