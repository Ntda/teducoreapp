using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Data.Entities;
using TeduNetcore.Data.IRepositories;
using TeduNetcore.Infrastructure.Intarfaces;

namespace TeduNetcore.Application.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(RoleManager<AppRole> roleManager, IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
        {
            _roleManager = roleManager;
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }

        public IList<RoleViewModel> GetAll()
        {
            IQueryable<AppRole> roles = _roleManager.Roles;
            return roles.ProjectTo<RoleViewModel>().ToList();
        }

        public IList<PermissionViewModel> GetPermissionByRole(Guid roleId)
        {
            IQueryable<Permission> permissions = _permissionRepository.FindAll();
            IQueryable<PermissionViewModel> query = from p in permissions.DefaultIfEmpty()
                                                    where p.RoleId.Equals(roleId)
                                                    select new PermissionViewModel
                                                    {
                                                        RoleId = roleId,
                                                        FunctionId = p.FunctionId,
                                                        CanCreate = p.CanCreate ? true : false,
                                                        CanDelete = p.CanDelete ? true : false,
                                                        CanRead = p.CanRead ? true : false,
                                                        CanUpdate = p.CanUpdate ? true : false
                                                    };
            return query.ToList();
        }

        public void SavePermission(IList<PermissionViewModel> listPermmission)
        {
            var roleId = listPermmission.FirstOrDefault().RoleId;
            // Find old permission
            IQueryable<Permission> oldPermissions = _permissionRepository.FindAll(p => p.RoleId.Equals(roleId));
            // remove all old permission
            if (oldPermissions.Any())
            {
                _permissionRepository.RemoveMultiple(oldPermissions.ToList());
            }

            List<Permission> permissionEntity = Mapper.Map<List<Permission>>(listPermmission);
            // Add new permission
            foreach (Permission permmission in permissionEntity)
            {
                _permissionRepository.Add(permmission);
            }
            _unitOfWork.Commit();
        }
    }
}
