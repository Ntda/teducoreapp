using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Data.Entities;
using TeduNetcore.Utilities.Dtos;

namespace TeduNetcore.Application.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CreateUser(UserViewModel userViewModel)
        {
            AppUser userEntity = Mapper.Map<UserViewModel, AppUser>(userViewModel);
            IdentityResult user = await _userManager.CreateAsync(userEntity, userViewModel.Password);
            if (user.Succeeded && userViewModel.Roles.Any())
            {
                AppUser appUser = await _userManager.FindByNameAsync(userEntity.UserName);
                if (appUser != null)
                {
                    await _userManager.AddToRolesAsync(appUser, userViewModel.Roles);
                }
                return true;
            }
            return false;
        }

        public PageResult<UserViewModel> GetAllPage(string keyWord, int indexCurrentPage, int pageSize)
        {
            IQueryable<AppUser> query = _userManager.Users;
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                query = query.Where(u => u.FullName.Contains(keyWord)
                                       || u.UserName.Contains(keyWord)
                                       || u.Email.Contains(keyWord));
            }
            int totalRow = query.Count();
            query = query.Skip((indexCurrentPage - 1) * pageSize)
                            .Take(pageSize);
            System.Collections.Generic.List<UserViewModel> user = query.Select(q => new UserViewModel
            {
                Id = q.Id,
                UserName = q.UserName,
                Avatar = q.Avatar,
                Email = q.Email,
                FullName = q.FullName,
                DateCreated = q.DateCreated,
                PhoneNumber = q.PhoneNumber
            }).ToList();
            PageResult<UserViewModel> paginationSet = new PageResult<UserViewModel>
            {
                TotalRow = totalRow,
                Result = user,
            };
            return paginationSet;
        }

        public async Task<bool> UpdateUser(UserViewModel userViewModel)
        {
            AppUser userEntity = Mapper.Map<UserViewModel, AppUser>(userViewModel);
            AppUser user = await _userManager.FindByIdAsync(userEntity.Id.ToString());
            System.Collections.Generic.IList<string> currentRoles = await _userManager.GetRolesAsync(user);
            System.Collections.Generic.IEnumerable<string> rolesNeedRemoved = currentRoles.Except(userViewModel.Roles);
            IdentityResult resultRemoveRoles = await _userManager.RemoveFromRolesAsync(user, rolesNeedRemoved);
            if (resultRemoveRoles.Succeeded)
            {
                IdentityResult resultUpdate = await _userManager.UpdateAsync(userEntity);
                if (resultUpdate.Succeeded)
                {
                    System.Collections.Generic.IEnumerable<string> rolesNeedAdd = userViewModel.Roles.Except(currentRoles);
                    IdentityResult resultAddRoles = await _userManager.AddToRolesAsync(userEntity, rolesNeedAdd);
                    return resultAddRoles.Succeeded;
                }
            }
            return false;
        }

        public async Task<bool> DeleteUser(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            IdentityResult result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}
