namespace SozlukApp.WebApp.Infrastructure.Services.Interfaces
{
    public interface IVoteService
    {
        Task CreateEntryCommentDownVote(Guid EntryCommentId);
        Task CreateEntryCommentUpVote(Guid EntryCommentId);
        Task CreateEntryDownVote(Guid EntryId);
        Task CreateEntryUpVote(Guid EntryId);
        Task DeleteEntryCommentVote(Guid entryCommentId);
        Task DeleteEntryVote(Guid entryId);
    }
}