using Blazored.LocalStorage;
using SozlukAppCommon.Infrastructure.Exceptions;
using SozlukAppCommon.Infrastructure.Results;
using SozlukAppCommon.Models.Queries;
using SozlukAppCommon.Models.RequestModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace SozlukApp.WebApp.Infrastructure.Services
{
    public class IdentityService
    {
        private readonly HttpClient client;
        private readonly ISyncLocalStorageService syncLocalStorageService;

        public IdentityService(HttpClient client, ISyncLocalStorageService syncLocalStorageService)
        {
            this.client = client;
            this.syncLocalStorageService = syncLocalStorageService;
        }

        public bool IsLoggedIn => string.IsNullOrEmpty(GetUserToken());

        //public string GetUserToken()
        //{
        //    return syncLocalStorageService;
        //}

        public async Task<bool> Login(LoginUserCommand command)
        {
            string responseStr;

            var httpResponse = await client.PostAsJsonAsync("/api/User/Login", command);

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

            if (!string.IsNullOrEmpty(response.Token))
            {
                syncLocalStorageService.SetToken(response.Token);
                syncLocalStorageService.SetUserName(response.UserName);
                syncLocalStorageService.SetUserId(response.Id);

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("");

                return true;
            }

            return false;

        }
    }
}
