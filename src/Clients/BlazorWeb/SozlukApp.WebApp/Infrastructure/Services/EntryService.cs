using SozlukApp.WebApp.Infrastructure.Services.Interfaces;
using SozlukApp.Common.Models.Page;
using SozlukApp.Common.Models.Queries;
using SozlukApp.Common.Models.QueryModels;
using SozlukApp.Common.Models.RequestModels;
using SozlukApp.Common.Models.ResponseModels;
using System.Net.Http.Json;

namespace SozlukApp.WebApp.Infrastructure.Services
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient client;

        public EntryService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<GetEntriesViewModel>> GetEntries()
        {
            var result = await client.GetFromJsonAsync<List<GetEntriesViewModel>>($"/api/Entry?todaysEntries=false&count=30");

            return result;
        }

        public async Task<GetEntryDetailViewModel> GetEntrieDetail(Guid entryId)
        {
            return await client.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/Entry/{entryId}");
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int currentPage, int pageSize)
        {
            return await client.
                GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/Entry/MainPageEntries?page={currentPage}&pageSize={pageSize}");
        }

        public async Task<PagedViewModel<GetUserEntriesViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null)
        {
            return await client.GetFromJsonAsync<PagedViewModel<GetUserEntriesViewModel>>($"/api/Entry/UserEntries?username={userName}&page={page}&pageSize={pageSize}");
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
        {
            return await client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"/api/Entry/Comments?entryId={entryId}&page={page}&pageSize={pageSize}");
        }

        public async Task<Guid> CreateEntry(CreateEntryCommand command)
        {
            var res = await client.PostAsJsonAsync($"/api/Entry/CreateEntry", command);

            if (!res.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await res.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
        {
            var res = await client.PostAsJsonAsync($"/api/Entry/CreateEntryComment", command);

            if (!res.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await res.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<List<SearchBySubjectViewModel>> Search(string searchText)
        {
            return await client.GetFromJsonAsync<List<SearchBySubjectViewModel>>($"/api/Entry/Search?searchText={searchText}");
        }
    }
}
