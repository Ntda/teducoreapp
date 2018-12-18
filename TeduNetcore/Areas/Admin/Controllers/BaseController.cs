using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TeduNetcore.Areas.Admin.Controllers
{
    [Area("admin")]
    public class BaseController : Controller
    {
        protected ILogger _logger { get; set; }
    }
}