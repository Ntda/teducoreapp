using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TeduNetcore.Application.Interfaces;

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
                var users = _uservice.GetAllPage(keyword, indexCurrentPage, pageSize);
                foreach (var user in users.Result)
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
    }
}