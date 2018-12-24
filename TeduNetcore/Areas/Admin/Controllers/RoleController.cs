using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeduNetcore.Application.Interfaces;

namespace TeduNetcore.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        private IRoleService _roleService;
        public RoleController(IRoleService roleService, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleService.GetAll();
            foreach (Application.ViewModels.RoleViewModel role in roles)
            {
                _logger.LogInformation(role.ToString());
            }
            return new OkObjectResult(roles);
        }
    }
}