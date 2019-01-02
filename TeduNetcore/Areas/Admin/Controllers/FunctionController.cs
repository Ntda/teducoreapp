using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Application.ViewModels;

namespace TeduNetcore.Areas.Admin.Controllers
{
    public class FunctionController : BaseController
    {
        private readonly IFunctionService _functionService;
        private readonly ILogger _logger;

        public FunctionController(IFunctionService functionService, ILogger<FunctionController> logger)
        {
            _logger = logger;
            _functionService = functionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<FunctionViewModel> functionsVM = await _functionService.GetAll();
            IEnumerable<FunctionViewModel> functionParent = functionsVM.Where(f => string.IsNullOrWhiteSpace(f.ParentId));
            IEnumerable<FunctionViewModel> functionChild = functionsVM.Except(functionParent);
            List<FunctionViewModel> result = new List<FunctionViewModel>();
            foreach (FunctionViewModel function in functionParent)
            {
                result.Add(function);
                BuildFunctionTree(function, functionChild, result);
            }

            // Logger
            foreach (FunctionViewModel r in result)
            {
                _logger.LogDebug(r.ToString());
            }
            return new OkObjectResult(result);
        }

        private void BuildFunctionTree(FunctionViewModel function, IEnumerable<FunctionViewModel> functionsVM, List<FunctionViewModel> result)
        {
            IEnumerable<FunctionViewModel> functionchild = functionsVM.Where(f => f.ParentId.Equals(function.Id));
            foreach (FunctionViewModel item in functionchild)
            {
                result.Add(item);
                BuildFunctionTree(item, functionsVM, result);
            }
        }
    }
}