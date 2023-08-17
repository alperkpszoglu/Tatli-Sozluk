namespace SozlukApp.Common.Events.Entry
{
    public class DeleteEntryCommentFavEvent
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}
