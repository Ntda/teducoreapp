﻿using System;
using System.Collections.Generic;
using System.Text;
using TeduNetcore.Application.ViewModels;

namespace TeduNetcore.Application.Interfaces
{
    public interface IRoleService
    {
        IList<RoleViewModel> GetAll();
    }
}
