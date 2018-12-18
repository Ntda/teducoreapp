using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Extensions;

namespace TeduNetcore.Areas.Admin.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private IFunctionService _functionService;
        private readonly ILogger<SideBarViewComponent> _ilogger;
        public SideBarViewComponent(IFunctionService functionService, ILogger<SideBarViewComponent> ilogger)
        {
            _functionService = functionService;
            _ilogger = ilogger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string roles = (User as ClaimsPrincipal).GetSpecificClaim("Roles");
            IList<FunctionViewModel> functionViewModels;
            if (roles.Split("-").Contains("Admin"))
            {
                functionViewModels = await _functionService.GetAll();
                foreach (FunctionViewModel functionVM in functionViewModels)
                {
                    _ilogger.LogDebug(functionVM.ToString());
                }
            }
            else
            {
                functionViewModels = new List<FunctionViewModel>();
                // TODO
            }

            return View(functionViewModels);
        }
    }
}
