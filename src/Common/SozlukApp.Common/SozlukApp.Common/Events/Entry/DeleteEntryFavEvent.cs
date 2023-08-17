namespace SozlukApp.Common.Events.Entry
{
    public class DeleteEntryFavEvent
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
    }
}
