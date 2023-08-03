namespace SozlukAppCommon.Events.Entry
{
    public class DeleteEntryFavEvent
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
    }
}
