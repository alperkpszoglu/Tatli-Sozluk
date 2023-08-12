using Blazored.LocalStorage;

namespace SozlukApp.WebApp.Infrastructure.Extensions
{
    public static class LocalStorageExtension
    {
        public const string TokenName = "token";
        public const string UserName = "username";
        public const string UserID = "userid";

        public static bool IsUserLoggedIn(this ISyncLocalStorageService localStorageService)
        {
            return !string.IsNullOrEmpty(GetToken(localStorageService));
        }

        public static string GetUserName(this ISyncLocalStorageService localStorageService)
        {
            return localStorageService.GetItem<string>(UserName);
        }

        public static async Task<string> GetUserName(this ILocalStorageService localStorageService)
        {
            return await localStorageService.GetItemAsync<string>(UserName);
        }

        public static void SetUserName(this ISyncLocalStorageService localStorageService, string value)
        {
            localStorageService.SetItem(UserName, value);
        }

        public static async Task SetUserName(this ILocalStorageService localStorageService, string value)
        {
            await localStorageService.SetItemAsync(UserName, value);
        }


        public static Guid GetUserId(this ISyncLocalStorageService localStorageService)
        {
            return localStorageService.GetItem<Guid>(UserID);
        }

        public static async Task<Guid> GetUserId(this ILocalStorageService localStorageService)
        {
            return await localStorageService.GetItemAsync<Guid>(UserID);
        }

        public static void SetUserId(this ISyncLocalStorageService localStorageService, Guid id)
        {
            localStorageService.SetItem(UserID, id);
        }

        public static async Task SetUserId(this ILocalStorageService localStorageService, Guid id)
        {
            await localStorageService.SetItemAsync(UserID, id);
        }


        public static string GetToken(this ISyncLocalStorageService localStorageService)
        {
            return localStorageService.GetItem<string>(TokenName);
        }

        public static void SetToken(this ISyncLocalStorageService localStorageService, string value)
        {
            localStorageService.SetItem(TokenName, value);
        }

        public static async Task SetToken(this ILocalStorageService localStorageService, string value)
        {
            await localStorageService.SetItemAsync(TokenName, value);
        }
    }
}
