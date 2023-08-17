namespace SozlukApp.Common.Events.Entry
{
    public class CreateEntryFavEvent
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }

    }
}
