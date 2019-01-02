using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Application.ViewModels;

namespace TeduNetcore.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _uservice;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _logger = logger;
            _uservice = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllPage(string keyword, int indexCurrentPage, int pageSize)
        {
            if (ModelState.IsValid)
            {
                Utilities.Dtos.PageResult<UserViewModel> users = _uservice.GetAllPage(keyword, indexCurrentPage, pageSize);
                foreach (UserViewModel user in users.Result)
                {
                    user.ToString();
                }
                return new OkObjectResult(users);
            }
            else
            {
                IEnumerable<ModelErrorCollection> error = ModelState.Values.Select(err => err.Errors);
                return new BadRequestObjectResult(error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveChange(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                if (userViewModel.Id == null)
                {
                    _logger.LogDebug("Execute create user");
                     await _uservice.CreateUser(userViewModel);
                }
                else
                {
                    _logger.LogDebug("Execute create user");
                    await _uservice.UpdateUser(userViewModel);
                }
                return new OkObjectResult(userViewModel);
            }
            else
            {
                var error = ModelState.Values.Select(err => err.Errors);
                return new BadRequestObjectResult(error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _uservice.DeleteUser(id);
            return new OkObjectResult(id);
        }
    }
}