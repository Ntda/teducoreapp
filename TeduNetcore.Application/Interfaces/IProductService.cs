using System;
using System.Collections;
using System.Collections.Generic;
using TeduNetcore.Application.ViewModels;

namespace TeduNetcore.Application.Interfaces
{
    public interface IProductService : IDisposable
    {
        IList<ProductViewModel> GetAll();
    }
}
