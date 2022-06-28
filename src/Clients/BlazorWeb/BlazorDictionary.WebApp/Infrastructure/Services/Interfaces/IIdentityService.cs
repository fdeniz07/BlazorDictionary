using BlazorDictionary.Common.Models.RequestModels;

namespace BlazorDictionary.WebApp.Infrastructure.Services.Interfaces
{
    public interface IIdentityService
    {
        bool IsLoggedIn { get; }

        string GetUserToken();

        string GetUserName();

        Guid GetUserId();

        Task<bool> Login(LoginUserCommand command);

        void Logout();
    }
}
