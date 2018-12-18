using Microsoft.AspNetCore.Mvc;

namespace TeduNetcore.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}