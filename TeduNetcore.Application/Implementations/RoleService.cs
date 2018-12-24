using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Data.Entities;

namespace TeduNetcore.Application.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IList<RoleViewModel> GetAll()
        {
            var roles = _roleManager.Roles;
            return roles.ProjectTo<RoleViewModel>().ToList();
        }
    }
}
