using System;
using System.Collections.Generic;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Utilities.Dtos;

namespace TeduNetcore.Application.Interfaces
{
    public interface IProductService : IDisposable
    {
        IList<ProductViewModel> GetAll();

        PageResult<ProductViewModel> GetAllPage(int? categoryId, string keyWord, int indexCurrentPage, int pageSize);
    }
}
