using BlazorDictionary.Common.Models.Queries;

namespace BlazorDictionary.WebApp.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailViewModel> GetUserDetail(Guid? id);
        Task<UserDetailViewModel> GetUserDetail(string userName);

        Task<bool> UpdateUser(UserDetailViewModel user);

        Task<bool> ChangeUserPassword(string oldPassword, string newPassword);
    }
}
