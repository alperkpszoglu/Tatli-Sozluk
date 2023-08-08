using SozlukApp.WebApp.Infrastructure.Services.Interfaces;
using SozlukAppCommon.ViewModels;

namespace SozlukApp.WebApp.Infrastructure.Services
{
    public class VoteService : IVoteService
    {
        private readonly HttpClient httpClient;

        public VoteService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateEntryUpVote(Guid EntryId)
        {
            await CreateEntryVote(EntryId, VoteType.UpVote);
        }

        public async Task CreateEntryDownVote(Guid EntryId)
        {
            await CreateEntryVote(EntryId, VoteType.DownVote);
        }

        public async Task CreateEntryCommentUpVote(Guid EntryCommentId)
        {
            await CreateEntryCommentVote(EntryCommentId, VoteType.UpVote);
        }

        public async Task CreateEntryCommentDownVote(Guid EntryCommentId)
        {
            await CreateEntryCommentVote(EntryCommentId, VoteType.DownVote);
        }

        public async Task DeleteEntryVote(Guid entryId)
        {
            var res = await httpClient.PostAsync($"/api/Vote/DeleteEntryVote/{entryId}", null);

            if (!res.IsSuccessStatusCode)
            {
                throw new Exception("DeteEntryVote error...");
            }
        }

        public async Task DeleteEntryCommentVote(Guid entryCommentId)
        {
            await httpClient.PostAsync($"/api/Vote/DeleteEntryCommentVote/{entryCommentId}", null);
        }

        private async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            return await httpClient.PostAsync($"/api/Vote/Entry/{entryId}?voteType={voteType}", null);
        }

        private async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            return await httpClient.PostAsync($"/api/Vote/EntryComment/{entryCommentId}?voteType={voteType}", null);
        }
    }
}
