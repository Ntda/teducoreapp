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

        public Task UpdateUser(UserViewModel userViewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
