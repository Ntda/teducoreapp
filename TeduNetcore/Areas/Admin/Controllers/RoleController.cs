using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Application.ViewModels;

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
            IList<RoleViewModel> roles = _roleService.GetAll();
            foreach (RoleViewModel role in roles)
            {
                _logger.LogInformation(role.ToString());
            }
            return new OkObjectResult(roles);
        }

        [HttpGet]
        public IActionResult GetPermission(Guid roleId)
        {
            IList<PermissionViewModel> permissions = _roleService.GetPermissionByRole(roleId);
            return new OkObjectResult(permissions);
        }

        [HttpPost]
        public IActionResult SavePermisssion(IList<PermissionViewModel> listPermmission)
        {
            try
            {
                _roleService.SavePermission(listPermmission);
                return Ok(200);
            }
            catch (Exception)
            {
                return new StatusCodeResult(400);
            }
        }
    }
}