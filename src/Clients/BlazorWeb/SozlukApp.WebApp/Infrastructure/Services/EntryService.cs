using SozlukAppCommon.Models.Queries;
using System.Net.Http.Json;

namespace SozlukApp.WebApp.Infrastructure.Services
{
    public class EntryService
    {
        private readonly HttpClient client;

        public EntryService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<GetEntriesViewModel>> GetEntries()
        {
            var result = await client.GetFromJsonAsync<List<GetEntriesViewModel>>("/api/Entry?todayEntries=false&count=30");
            
            return result;
        }
    }
}
