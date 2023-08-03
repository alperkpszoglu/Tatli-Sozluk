namespace SozlukAppCommon.Events.EntryComment
{
    public class CreateEntryCommentFavEvent
    {
        public Guid EntryCommentId{ get; set; }

        public Guid CreatedBy { get; set; }
    }
}
