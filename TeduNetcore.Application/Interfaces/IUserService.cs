using System;
using System.Collections.Generic;
using System.Text;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Utilities.Dtos;

namespace TeduNetcore.Application.Interfaces
{
    public interface IUserService
    {
        PageResult<UserViewModel> GetAllPage(string keyWord, int indexCurrentPage, int pageSize);
    }
}
