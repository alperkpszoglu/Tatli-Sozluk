using SozlukApp.WebApp.Infrastructure.Services.Interfaces;
using SozlukAppCommon.Events.User;
using SozlukAppCommon.Infrastructure.Exceptions;
using SozlukAppCommon.Infrastructure.Results;
using SozlukAppCommon.Models.QueryModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace SozlukApp.WebApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient client;

        public UserService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<UserDetailViewModel> GetUserDetail(Guid id)
        {
            var user = await client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{id}");

            return user;
        }

        public async Task<UserDetailViewModel> GetUserDetail(string userName)
        {
            var user = await client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/UserName/{userName}");

            return user;
        }

        public async Task<bool> UpdateUser(UserDetailViewModel user)
        {
            var res = await client.PostAsJsonAsync("/api/user/update", user);

            return res.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeUserPassword(string newPassword, string oldPassword)
        {
            var command = new ChangeUserPasswordCommand(null, newPassword, oldPassword);

            var httpResponse = await client.PostAsJsonAsync("/api/User/ChangePassword", command);
            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var response = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(response);
                    response = validation.FlattenErrors;
                    throw new DbValidationException(response);
                }
                return false;
            }

            return httpResponse.IsSuccessStatusCode;
        }


    }
}
