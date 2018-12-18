using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeduNetcore.Application.ViewModels;

namespace TeduNetcore.Application.Interfaces
{
    public interface IFunctionService : IDisposable
    {
        Task<List<FunctionViewModel>> GetAll();
        IList<FunctionViewModel> GetPermissionById(Guid userId);
    }
}
