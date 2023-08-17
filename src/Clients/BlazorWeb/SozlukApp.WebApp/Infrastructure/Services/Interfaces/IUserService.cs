using SozlukApp.Common.Models.QueryModels;

namespace SozlukApp.WebApp.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> ChangeUserPassword(string newPassword, string oldPassword);
        Task<UserDetailViewModel> GetUserDetail(Guid id);
        Task<UserDetailViewModel> GetUserDetail(string userName);
        Task<bool> UpdateUser(UserDetailViewModel user);
    }
}