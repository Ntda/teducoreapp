using System.Threading.Tasks;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Utilities.Dtos;

namespace TeduNetcore.Application.Interfaces
{
    public interface IUserService
    {
        PageResult<UserViewModel> GetAllPage(string keyWord, int indexCurrentPage, int pageSize);

        Task<bool> CreateUser(UserViewModel userViewModel);

        Task<bool> UpdateUser(UserViewModel userViewModel);

        Task<bool> DeleteUser(string id);
    }
}
