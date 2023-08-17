using Blazored.LocalStorage;
using SozlukApp.WebApp.Infrastructure.Extensions;
using SozlukApp.WebApp.Infrastructure.Services.Interfaces;
using SozlukApp.Common.Infrastructure.Exceptions;
using SozlukApp.Common.Infrastructure.Results;
using SozlukApp.Common.Models.Queries;
using SozlukApp.Common.Models.RequestModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace SozlukApp.WebApp.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient client;
        private readonly ISyncLocalStorageService syncLocalStorageService;

        public IdentityService(HttpClient client, ISyncLocalStorageService syncLocalStorageService)
        {
            this.client = client;
            this.syncLocalStorageService = syncLocalStorageService;
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public string GetUserToken()
        {
            return syncLocalStorageService.GetToken();
        }

        public string GetUserName()
        {
            return syncLocalStorageService.GetUserName();
        }

        public Guid GetUserId()
        {
            return syncLocalStorageService.GetUserId();
        }

        public async Task<bool> Login(LoginUserCommand command)
        {
            string responseStr;

            var httpResponse = await client.PostAsJsonAsync("/api/User/Login", command);

            // if there is an error
            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DbValidationException(responseStr);
                }

                return false;
            }

            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);

            if (!string.IsNullOrEmpty(response.Token)) // the user has logged in
            {
                syncLocalStorageService.SetToken(response.Token);
                syncLocalStorageService.SetUserName(response.UserName);
                syncLocalStorageService.SetUserId(response.Id);

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", response.UserName);

                return true;
            }

            return false;
        }

        public void Logout()
        {
            syncLocalStorageService.RemoveItem(LocalStorageExtension.TokenName);
            syncLocalStorageService.RemoveItem(LocalStorageExtension.UserName);
            syncLocalStorageService.RemoveItem(LocalStorageExtension.UserID);

            client.DefaultRequestHeaders.Authorization = null;
        }


    }
}
